using UnityEngine;

public class GhostTrack : Ghost
{
    public Transform wayPoint;
    public Transform target;
    public float speed;
    public PacmanMove pacman;
    private Transform frontPoint; //避免死循环移动，不走回头路
    private Transform iniWayPoint;

    private Transform iniTarget;

    // Use this for initialization
    void Awake()
    {
        iniWayPoint = wayPoint;
        iniTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || WinCondiction.m_isWin || GameManager.m_paused) return;
        var p = Vector2.MoveTowards(transform.position, wayPoint.position, speed);
        transform.position = p;
        if (!(Vector2.Distance(transform.position, wayPoint.position) < 0.1f)) return;
        var temp = wayPoint;
        ChangeWayPoint(); //到达路点，变更下一个路点
        frontPoint = temp;
        Vector2 dir = wayPoint.position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    private void ChangeWayPoint()
    {
        if (pacman.m_pacmanState == PacmanMove.PacmanNormal || pacman.m_pacmanState == PacmanMove.PacmanHurt
        ) //吃豆人正常状态或受伤状态下，接近吃豆人
        {
            var nextWay = wayPoint; //下一个路点，设为上一路点仅是为了避免报空值错误
            var distance = float.MaxValue; //初始距离设为最大值
            var nextWays = wayPoint.GetComponent<WayPoint>();
            foreach (var wayPoint in nextWays.nextPoint) //遍历该路点临近所有路点，找出与目标点最近的路点，设置为新路点
            {
                if (!(Vector2.Distance(wayPoint.position, target.position) < distance) ||
                    wayPoint == frontPoint) continue; //不走回头路
                distance = Vector2.Distance(wayPoint.position, target.position);
                nextWay = wayPoint;
            }

            wayPoint = nextWay;
        }
        else if (pacman.m_pacmanState == PacmanMove.PacmanInvincible) //吃豆人无敌状态下，远离吃豆人
        {
            var nextWay = wayPoint; //下一个路点，设为上一路点仅是为了避免报空值错误
            float distance = 0; //初始距离设为最小值
            var nextWays = wayPoint.GetComponent<WayPoint>();
            foreach (var wayPoint in nextWays.nextPoint) //遍历该路点临近所有路点，找出与吃豆人最远的路点，设置为新路点
            {
                if (!(Vector2.Distance(wayPoint.position, pacman.transform.position) > distance)) continue;
                distance = Vector2.Distance(wayPoint.position, pacman.transform.position);
                nextWay = wayPoint;
            }

            wayPoint = nextWay;
        }
    }

    public void OnDisable()
    {
        wayPoint = iniWayPoint;
        target = iniTarget;
    }
}