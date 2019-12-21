using System.Collections.Generic;
using UnityEngine;

public class ClydeTrack : MonoBehaviour
{
    public Transform pacman;
    public List<Transform> corners;
    private GhostTrack ghostTrack;
    private bool caculateCorner;

    void Start()
    {
        ghostTrack = GetComponent<GhostTrack>();
    }

    void Update()
    {
        if (pacman == null) return;
        if (Vector2.Distance(pacman.position, transform.position) > 15) //距离大于15时，以吃豆人为目标
        {
            ghostTrack.target = pacman;
            caculateCorner = false;
        }
        else if (caculateCorner == false) //距离小于15时，走向最近的角落
        {
            var distance = float.MaxValue;
            var nearCorner = transform;
            foreach (var corner in corners)
            {
                if (!(Vector2.Distance(corner.position, transform.position) < distance)) continue;
                distance = Vector2.Distance(corner.position, transform.position);
                nearCorner = corner;
            }
            ghostTrack.target = nearCorner;
            caculateCorner = true;
        }
    }
}