using System.Collections.Generic;
using UnityEngine;

public class GhostNavi : Ghost
{
    public Transform pacman;
    private List<Vector2> path;
    public float speed = 0.3f;
    private float timer;

    void Start()
    {
        //path = NavMesh2D.GetSmoothedPath(transform.position, m_pacman.position);
    }

    void Update()
    {
        if (pacman == null) return;
        
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            //path = NavMesh2D.GetSmoothedPath(transform.position, m_pacman.position);
            timer = 0;
            var dir = path[1] - (Vector2) transform.position;
            if (Mathf.Abs(dir.x) - Mathf.Abs(dir.y) > 0)
            {
                GetComponent<Animator>().SetFloat("DirX", dir.x);
                GetComponent<Animator>().SetFloat("DirY", 0);
            }
            else
            {
                GetComponent<Animator>().SetFloat("DirX", 0);
                GetComponent<Animator>().SetFloat("DirY", dir.y);
            }
        }

        if (path == null || path.Count == 0) return;
        
        transform.position = Vector2.MoveTowards(transform.position, path[0], speed);
        if (Vector2.Distance(transform.position, path[0]) < 0.01f)
        {
            path.RemoveAt(0);
        }
    }

    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.tag == "Player")
    //        Destroy(collider.gameObject);
    //}
}