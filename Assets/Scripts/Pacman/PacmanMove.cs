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
    public int m_pacmanState = 0;
    public float speed = 0.4f;

    /*
    角色属性
     */
    public static int m_life = 3; //生命
    public static int m_maxlife = 3; //最大生命
    public static bool PACMAN_CANMOVE = true; //可以移动
    public const float m_invicibleTime = 5; //无敌时间
    public const float m_invicibleFlashTime = 2.5f; //无敌即将结束闪烁提示时间

    private float m_invicibleTimer = 0; //无敌计时
    private float m_invicibleFlashTimer = 0; //无敌动画计时
    Vector2 dest = Vector2.zero; //角色坐标

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
                PACMAN_CANMOVE = Valid((Vector2.up + Vector2.left * 0.3f) * 2) &&
                                 Valid((Vector2.up + Vector2.right * 0.3f) * 2);
                break;
            case MoveRight:
                PACMAN_CANMOVE = Valid((Vector2.right + Vector2.up * 0.3f) * 2) &&
                                 Valid((Vector2.right + Vector2.down * 0.3f) * 2);
                break;
            case MoveDown:
                PACMAN_CANMOVE = Valid((Vector2.down + Vector2.left * 0.3f) * 2) &&
                                 Valid((Vector2.down + Vector2.right * 0.3f) * 2);
                break;
            case MoveLeft:
                PACMAN_CANMOVE = Valid((Vector2.left + Vector2.up * 0.3f) * 2) &&
                                 Valid((Vector2.left + Vector2.down * 0.3f) * 2);
                break;
        }

//        print("CanMove:" + PACMAN_CANMOVE);
    }

    private void FixedUpdate()
    {
        if (GameManager.m_paused || WinCondiction.m_isWin) return;

        var p = Vector2.MoveTowards(transform.position, dest, speed);
        transform.position = p;

        if ((Vector2) transform.position == dest)
        {
//            print("Move:" + m_PacmanMoveState);
            //使用虚拟摇杆时运算
            if (m_PacmanMoveState == MoveUp && Valid((Vector2.up + Vector2.left * 0.3f) * 2) &&
                Valid((Vector2.up + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.up;
            if (m_PacmanMoveState == MoveRight && Valid((Vector2.right + Vector2.up * 0.3f) * 2) &&
                Valid((Vector2.right + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.right;
            if (m_PacmanMoveState == MoveDown && Valid((Vector2.down + Vector2.left * 0.3f) * 2) &&
                Valid((Vector2.down + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.up;
            if (m_PacmanMoveState == MoveLeft && Valid((Vector2.left + Vector2.up * 0.3f) * 2) &&
                Valid((Vector2.left + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.right;

            //使用键盘按键时运算
            if (Input.GetKey(KeyCode.UpArrow) && Valid((Vector2.up + Vector2.left * 0.3f) * 2) &&
                Valid((Vector2.up + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && Valid((Vector2.right + Vector2.up * 0.3f) * 2) &&
                Valid((Vector2.right + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && Valid((Vector2.down + Vector2.left * 0.3f) * 2) &&
                Valid((Vector2.down + Vector2.right * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && Valid((Vector2.left + Vector2.up * 0.3f) * 2) &&
                Valid((Vector2.left + Vector2.down * 0.3f) * 2))
                dest = (Vector2) transform.position - Vector2.right;
        }

        /*
         * 根据移动方向剩余的距离dir中x、y确定动画的播放
         */
        var dir = dest - (Vector2) transform.position;
        //得到动画组件
        var animator = GetComponent<Animator>();
        animator.SetFloat("DirX", dir.x);
        animator.SetFloat("DirY", dir.y);
    }

    /// <summary>
    /// 碰撞检测，检查目标点的有效性
    /// </summary>
    /// <param name="dir">增加的距离</param>
    /// <returns>true有效</returns>
    private bool Valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        var hit = Physics2D.Linecast(pos + dir, pos);
        if (hit.collider == null) return true;

        return hit.collider.gameObject.layer != LayerMask.NameToLayer("Wall");
//               && hit.collider.gameObject.layer != LayerMask.NameToLayer("Door");
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