using UnityEngine;

public class GhostOut : MonoBehaviour
{
    public Transform[] wanderPoint;
    public int outCount;
    int index;
    public float speed = 0.1f;

    void Start()
    {
        GetComponent<GhostTrack>().enabled = false;
    }

    void Update()
    {
        if (GameManager.gameManager.m_eatedDot < outCount)
        {//吃豆人没有达到点数目标，幽灵一直等到，直到出去为止
            var p = Vector2.MoveTowards(transform.position, wanderPoint[index].position, speed);
            transform.position = p;
            if (!(Vector2.Distance(transform.position, wanderPoint[index].position) < 0.1f)) return;
            index = (index + 1) % wanderPoint.Length;
            GetComponent<Animator>().SetFloat("DirX", wanderPoint[index].position.x - transform.position.x);
        }
        else
        {//当幽灵出去后将不再更新这个组件，并且启动GhostTrack的更新
            GetComponent<GhostTrack>().enabled = true;
            enabled = false;
        }
    }
}