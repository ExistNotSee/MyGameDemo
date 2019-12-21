using UnityEngine;

public class GhostMove : Ghost {
    public Transform[] wayPoints;
    public int curPos;
    public PacmanMove pacman;
    public float speed = 0.3f;
    public bool isToNear;
    public Transform nearWayPoint;
	
    void FixedUpdate()
    {
        if (pacman.m_pacmanState == PacmanMove.PacmanNormal)
        {
            isToNear = false;
            if (transform.position != wayPoints[curPos].position)
            {
                var p = Vector2.MoveTowards(transform.position, wayPoints[curPos].position, speed);
                transform.position = p;
            }
            else
            {
                curPos = (curPos + 1) % wayPoints.Length;
            }

            Vector2 dir = wayPoints[curPos].position - transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
        else if(pacman.m_pacmanState == PacmanMove.PacmanInvincible)
        {
            if (isToNear == false)
            {
                nearWayPoint = EscapeToNearWayPoint();
                isToNear = true;
            }
            var p = Vector2.MoveTowards(transform.position, nearWayPoint.position, speed);
            transform.position = p;
            if (!(Vector2.Distance(transform.position, nearWayPoint.position) < 0.1f)) return;
            nearWayPoint = ChangeFarWayPoint(pacman.transform,nearWayPoint);//到达路点，变更下一个路点
            Vector2 dir = nearWayPoint.position - transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }
  
    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.tag == "Player")
    //        Destroy(collider.gameObject);
    //}
}
