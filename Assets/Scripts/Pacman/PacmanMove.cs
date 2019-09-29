using UnityEngine;
public class PacmanMove : MonoBehaviour {
    public static int pacmanNormal = 0;
    public static int pacmanInvicible = 1;
    public static int pacmanHurt = 2;
    public static int m_life = 3;
    public static int m_maxlife = 3;
    public static int m_PacmanMoveState = 0;
    public static int MOVE_NONE = 0;
    public static int MOVE_UP = 1;
    public static int MOVE_DOWN = 2;
    public static int MOVE_LEFT = 3;
    public static int MOVE_RIGHT = 4;
    public static bool PACMAN_CANMOVE = true;
    public const float m_invicibleTime = 5;
    public const float m_invicibleFlashTime = 2.5f;
    public float speed = 0.4f;
    public int m_pacmanState = 0;
    private float m_invicibleTimer = 0;
    private float m_invicibleFlashTimer = 0;
    Vector2 dest = Vector2.zero;
    void Start () {

    }

}