using UnityEngine;

public class Ghost : MonoBehaviour
{
    public static GameObject restartGameObject;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var pacmanState = other.transform.GetComponent<PacmanMove>().m_pacmanState;
        if (pacmanState == PacmanMove.PacmanNormal)
        {
            other.GetComponent<PacmanMove>().ChangeState(PacmanMove.PacmanHurt);
            PacmanMove.m_life--;
            if (PacmanMove.m_life > 0) return;
            
            //生命为空时结束游戏
            GameManager.gameManager.SaveHighScore();
            WinCondiction.Instant.GameOverAudio(false);
            restartGameObject.SetActive(true);
            Destroy(other.gameObject);
        }
        else if (pacmanState == PacmanMove.PacmanInvincible)
        {
            GameManager.gameManager.addScore(100);
            GameManager.gameManager.ghostRevenge(gameObject);
            gameObject.SetActive(false);
        }
    }
    
    public Transform EscapeToNearWayPoint()
    {
        var wayPoints =  GameObject.FindGameObjectsWithTag("WayPoint");
        var nearWay = transform;
        var distance = float.MaxValue;
        foreach (var item in wayPoints)
        {
            if (!(Vector2.Distance(item.transform.position, transform.position) < distance)) continue;
            distance = Vector2.Distance(item.transform.position, transform.position);
            nearWay = item.transform;
        }
        return nearWay;
    }

    public Transform ChangeFarWayPoint(Transform pacman,Transform nowPosition)
    {
        var nextWay = transform;//下一个路点，设为上一路点仅是为了避免报空值错误
        float distance = 0;//初始距离设为最大值
        var nextWays = nowPosition.GetComponent<WayPoint>();
        foreach (var wayPoint in nextWays.nextPoint)//遍历该路点临近所有路点，找出与吃豆人最近的路点，设置为新路点
        {
            if (!(Vector2.Distance(wayPoint.position, pacman.position) > distance)) continue;
            distance = Vector2.Distance(wayPoint.position, pacman.position);
            nextWay = wayPoint;
        }
        return nextWay;
    }
}