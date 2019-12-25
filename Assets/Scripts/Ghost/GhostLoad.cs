using UnityEngine;

public class GhostLoad : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
    }
}
