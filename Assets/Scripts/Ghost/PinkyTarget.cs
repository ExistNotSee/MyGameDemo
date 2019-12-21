using UnityEngine;

public class PinkyTarget : MonoBehaviour
{
    public Transform pacman;
    public Transform dirPos;

    private GhostTrack track;

    // Use this for initialization
    void Start()
    {
        track = GetComponent<GhostTrack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pacman == null) return;
        track.target = Vector2.Distance(pacman.position, transform.position) > 15 ? dirPos : pacman;
    }
}