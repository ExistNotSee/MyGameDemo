using System;
using UnityEngine;

public class PacmanDot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
//        Debug.Log("PacmanDot"+gameObject.name+","+other.name);
        if (string.Compare(other.tag, "Player", StringComparison.Ordinal) != 0) return;
        GameManager.gameManager.addScore(1);
        GameManager.gameManager.EatDot();
        Destroy(gameObject);
    }
}