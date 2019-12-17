using System;
using UnityEngine;

public class SuperDot : MonoBehaviour
{
    public PacmanMove _pacmanMove;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (string.Compare(other.tag, "Player", StringComparison.Ordinal) != 0) return;
        GameManager.gameManager.addScore(10);
        _pacmanMove.ChangeState(PacmanMove.PacmanInvincible);
        Destroy(gameObject);
    }
}