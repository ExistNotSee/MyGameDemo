using UnityEngine;

public class InkyTrackPoint : MonoBehaviour
{
    public Transform blinky;
    public Transform inkyPos;
    public Transform pacman;
    public Transform trackPos;
    private GhostTrack track;

    void Start()
    {
        track = transform.GetComponent<GhostTrack>();
    }

    void Update()
    {
        if (pacman == null) return;
        
        if (blinky == null)
        {
            track.target = pacman;
        }
        else
        {
            if (Vector2.Distance(pacman.position, transform.position) > 15)
            {
                //做InkyPos和Bliky之间的延长线
                var position = blinky.position;
                var position1 = inkyPos.position;
                var k = (position1.y - position.y) / (position1.x - position.x);
                var distance = Vector2.Distance(position1, position);
                var angle = Mathf.Atan(k);
                var x = distance * Mathf.Cos(angle) + position1.x;
                var y = distance * Mathf.Sin(angle) + position1.y;
                trackPos.position = new Vector2(x, y);
                track.target = trackPos;
            }
            else
            {
                track.target = pacman;
            }
        }
    }
}