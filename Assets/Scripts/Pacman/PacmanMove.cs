using System;
using System.Collections;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    public const int pacmanNormal = 0; //正常
    public const int pacmanInvincible = 1; //无敌
    public const int pacmanHurt = 2; //受伤

    /*移动 */
    public static int m_PacmanMoveState = 0; //移动状态
    public static int MOVE_NONE = 0;
    public static int MOVE_UP = 1;
    public static int MOVE_DOWN = 2;
    public static int MOVE_LEFT = 3;

    public static int MOVE_RIGHT = 4;

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
        m_pacmanState = pacmanNormal;
    }

    void Update()
    {
        if (m_pacmanState == pacmanInvincible)
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
                ChangeState(pacmanNormal);
            }
        }

        if (m_PacmanMoveState == MOVE_UP)
        {
            PACMAN_CANMOVE = valid((Vector2.up + Vector2.left * 0.3f) * 2) &&
                             valid((Vector2.up + Vector2.right * 0.3f) * 2);
        }
        else if (m_PacmanMoveState == MOVE_RIGHT)
        {
            PACMAN_CANMOVE = valid((Vector2.right + Vector2.up * 0.3f) * 2) &&
                             valid((Vector2.right + Vector2.down * 0.3f) * 2);
        }
        else if (m_PacmanMoveState == MOVE_DOWN)
        {
            PACMAN_CANMOVE = valid((Vector2.down + Vector2.left * 0.3f) * 2) &&
                             valid((Vector2.down + Vector2.right * 0.3f) * 2);
        }
        else if (m_PacmanMoveState == MOVE_LEFT)
        {
            PACMAN_CANMOVE = valid((Vector2.left + Vector2.up * 0.3f) * 2) &&
                             valid((Vector2.left + Vector2.down * 0.3f) * 2);
        }

        print("CanMove:" + PACMAN_CANMOVE);
    }

    private void FixedUpdate()
    {
//        if (GameManager.m_paused == true || WinCondiction.m_isWin == true) {
//            return;
//        }
        var p = Vector2.MoveTowards(transform.position, dest, speed);
        transform.position = p;
        if ((Vector2) transform.position == dest)
        {
            print("Move:" + m_PacmanMoveState);
            if (m_PacmanMoveState == MOVE_UP && valid((Vector2.up + Vector2.left * 0.3f) * 2) &&
                valid((Vector2.up + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.up;
            if (m_PacmanMoveState == MOVE_RIGHT && valid((Vector2.right + Vector2.up * 0.3f) * 2) &&
                valid((Vector2.right + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.right;
            if (m_PacmanMoveState == MOVE_DOWN && valid((Vector2.down + Vector2.left * 0.3f) * 2) &&
                valid((Vector2.down + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.up;
            if (m_PacmanMoveState == MOVE_LEFT && valid((Vector2.left + Vector2.up * 0.3f) * 2) &&
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
            case pacmanNormal:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case pacmanInvincible:
                m_invicibleTimer += m_invicibleTime;
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case pacmanHurt:
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
        ChangeState(pacmanNormal);
    }

    /// <summary>
    /// 受伤mo's'
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
        ChangeState(pacmanNormal);
    }
}