using UnityEngine;
public class PacmanMove : MonoBehaviour {
    public static int pacmanNormal = 0; //正常
    public static int pacmanInvincible = 1; //无敌
    public static int pacmanHurt = 2; //受伤
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
    public static int m_life = 3;
    public static int m_maxlife = 3;
    public static bool PACMAN_CANMOVE = true;
    public const float m_invicibleTime = 5; //无敌时间
    public const float m_invicibleFlashTime = 2.5f; //无敌即将结束闪烁提示时间
    public float speed = 0.4f;
    public int m_pacmanState = 0;
    private float m_invicibleTimer = 0;
    private float m_invicibleFlashTimer = 0;
    Vector2 dest = Vector2.zero;
    void Start () {
        dest = transform.position;
        m_pacmanState = pacmanNormal;
    }
    void Update () {
        if (m_pacmanState == pacmanInvincible) {
            m_invicibleTimer -= Time.deltaTime;
            if (m_invicibleTimer <= m_invicibleFlashTime) {
                m_invicibleFlashTimer += Time.deltaTime;
                if (m_invicibleFlashTimer >= 0.5f) {
                    if (GetComponent<SpriteRenderer> ().color == Color.red) {
                        GetComponent<SpriteRenderer> ().color = Color.white;
                    } else {
                        GetComponent<SpriteRenderer> ().color = Color.red;
                    }
                    m_invicibleFlashTimer = 0;
                }
            }
            if(m_invicibleTimer<=0){
                m_invicibleTimer = 0;                
            }
        }
        if(m_PacmanMoveState==MOVE_UP){
        }

    }
}