using UnityEngine;

public class GhostOut : MonoBehaviour {
    public Transform[] wanderPoint;
    public int outCount;
    int index;
    public float speed = 0.1f;
	void Start () {
        GetComponent<GhostTrack>().enabled = false;
	}
	
	void Update () {
        if (GameManager.gameManager.m_eatedDot < outCount)
        {
            var p = Vector2.MoveTowards(transform.position, wanderPoint[index].position, speed);
            transform.position = p;
            if (!(Vector2.Distance(transform.position, wanderPoint[index].position) < 0.1f)) return;
            index = (index + 1) % wanderPoint.Length;
            GetComponent<Animator>().SetFloat("DirX", wanderPoint[index].position.x - transform.position.x);
        }
        else
        {
            GetComponent<GhostTrack>().enabled = true;
            enabled = false;
        }
	}
}
