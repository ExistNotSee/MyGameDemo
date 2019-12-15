using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanDot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("----");
        if (String.Compare(other.tag, "Player", StringComparison.Ordinal) != 0) return;
        GameManager.gameManager.addScore(1);
        GameManager.gameManager.EatDot();
        Destroy(gameObject);
    }
}