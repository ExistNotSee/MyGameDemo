using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public static GameObject restartGameObject;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var pacmanState = other.transform.GetComponent<PacmanMove>().m_pacmanState;
        if (pacmanState == PacmanMove.pacmanNormal)
        {
            PacmanMove.m_life--;
            if (PacmanMove.m_life <= 0) 
            {
                GameManager.gameManager.SaveHighScore();
                WinCondiction.Instant.Death();
                restartGameObject.SetActive(true);
                Destroy(other.gameObject);
            }
            else
            {
                other.GetComponent<PacmanMove>().ChangeState(PacmanMove.pacmanHurt);
            }
        }
        else if (pacmanState == PacmanMove.pacmanInvincible)
        {
            GameManager.gameManager.addScore(100);
            GameManager.gameManager.ghostRevenge(gameObject);
            gameObject.SetActive(false);
        }
    }
}