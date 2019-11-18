using System;
using System.Collections;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public const int PacmanNormal = 0; //正常
    public const int PacmanInvincible = 1; //无敌
    public const int PacmanHurt = 2; //受伤

    /*移动 */
    public static int m_PacmanMoveState = 0; //移动状态
    public const int MoveNone = 0;
    public const int MoveUp = 1;
    public const int MoveDown = 2;
    public const int MoveLeft = 3;
    public const int MoveRight = 4;

    /*
    角色属性
     */
    public static int m_life = 3; //生命
    public static int m_maxlife = 3; //最大生命
    public static bool PACMAN_CANMOVE = true; //可以移动
    public const float m_invicibleTime = 5; //无敌时间
    public const float m_invicibleFlashTime = 2.5f; //无敌即将结束闪烁提示时间
    public float speed = 0.4f;
    public int m_pacmanState = 0;
    private float m_invicibleTimer = 0;
    private float m_invicibleFlashTimer = 0;
    Vector2 dest = Vector2.zero;

    void Start()
    {
        dest = transform.position;
        m_pacmanState = PacmanNormal;
    }

    void Update()
    {
        if (m_pacmanState == PacmanInvincible)
        {
            m_invicibleTimer -= Time.deltaTime;
            if (m_invicibleTimer <= m_invicibleFlashTime)
            {
                m_invicibleFlashTimer += Time.deltaTime;
                if (m_invicibleFlashTimer >= 0.5f)
                {
                    GetComponent<SpriteRenderer>().color =
                        GetComponent<SpriteRenderer>().color == Color.red ? Color.white : Color.red;
                    m_invicibleFlashTimer = 0;
                }
            }

            if (m_invicibleTimer <= 0)
            {
                m_invicibleTimer = 0;
                ChangeState(PacmanNormal);
            }
        }

        switch (m_PacmanMoveState)
        {
            case MoveUp:
                PACMAN_CANMOVE = valid((Vector2.up + Vector2.left * 0.3f) * 2) &&
                                 valid((Vector2.up + Vector2.right * 0.3f) * 2);
                break;
            case MoveRight:
                PACMAN_CANMOVE = valid((Vector2.right + Vector2.up * 0.3f) * 2) &&
                                 valid((Vector2.right + Vector2.down * 0.3f) * 2);
                break;
            case MoveDown:
                PACMAN_CANMOVE = valid((Vector2.down + Vector2.left * 0.3f) * 2) &&
                                 valid((Vector2.down + Vector2.right * 0.3f) * 2);
                break;
            case MoveLeft:
                PACMAN_CANMOVE = valid((Vector2.left + Vector2.up * 0.3f) * 2) &&
                                 valid((Vector2.left + Vector2.down * 0.3f) * 2);
                break;
        }

        print("CanMove:" + PACMAN_CANMOVE);
    }

    private void FixedUpdate()
    {
        if (GameManager.m_paused || WinCondiction.m_isWin)
        {
            return;
        }

        var p = Vector2.MoveTowards(transform.position, dest, speed);
        transform.position = p;
        if ((Vector2) transform.position == dest)
        {
            print("Move:" + m_PacmanMoveState);
            if (m_PacmanMoveState == MoveUp && valid((Vector2.up + Vector2.left * 0.3f) * 2) &&
                valid((Vector2.up + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.up;
            if (m_PacmanMoveState == MoveRight && valid((Vector2.right + Vector2.up * 0.3f) * 2) &&
                valid((Vector2.right + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.right;
            if (m_PacmanMoveState == MoveDown && valid((Vector2.down + Vector2.left * 0.3f) * 2) &&
                valid((Vector2.down + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.up;
            if (m_PacmanMoveState == MoveLeft && valid((Vector2.left + Vector2.up * 0.3f) * 2) &&
                valid((Vector2.left + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.right;

            if (Input.GetKey(KeyCode.UpArrow) && valid((Vector2.up + Vector2.left * 0.3f) * 2) &&
                valid((Vector2.up + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid((Vector2.right + Vector2.up * 0.3f) * 2) &&
                valid((Vector2.right + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid((Vector2.down + Vector2.left * 0.3f) * 2) &&
                valid((Vector2.down + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && valid((Vector2.left + Vector2.up * 0.3f) * 2) &&
                valid((Vector2.left + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.right;
        }

        var dir = dest - (Vector2) transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        var hit = Physics2D.Linecast(pos + dir, pos);
        if (hit.collider == null)
        {
            return true;
        }

        return hit.collider.gameObject.layer != LayerMask.NameToLayer("wall") &&
               hit.collider.gameObject.layer != LayerMask.NameToLayer("Door");
    }

    /// <summary>
    /// 改变吃豆人角色的状态 
    /// </summary>
    /// <param name="state">目标状态码</param>
    public void ChangeState(int state)
    {
        m_pacmanState = state;
        switch (state)
        {
            case PacmanNormal:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case PacmanInvincible:
                m_invicibleTimer += m_invicibleTime;
                GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(Invicible());
                break;
            default:
                StartCoroutine(Hurt());
                break;
        }
    }

    /// <summary>
    /// 无敌模式
    /// </summary>
    /// <returns></returns>
    IEnumerator Invicible()
    {
        yield return new WaitForSeconds(5);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        ChangeState(PacmanNormal);
    }

    /// <summary>
    /// 受伤
    /// </summary>
    /// <returns></returns>
    IEnumerator Hurt()
    {
        Debug.LogWarning("-----hurt----1");
        var color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        Debug.LogWarning("-----hurt----2");
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        Debug.LogWarning("-----hurt----3");
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        ChangeState(PacmanNormal);
    }
}