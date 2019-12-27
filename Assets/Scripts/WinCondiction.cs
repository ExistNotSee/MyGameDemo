using UnityEngine;

public class WinCondiction : MonoBehaviour
{
    public static bool m_isWin;
    public static WinCondiction Instant;

    public GameObject m_winCanvas;
    public Transform m_soundTransform;
    public AudioClip m_winAudio;
    public AudioClip m_deathAudio;
    private bool m_winAudioPlayed = false;

    void Start()
    {
        Instant = this;
        m_isWin = false;
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Dot") != null) return;
        m_isWin = true;
        var level = int.Parse(PlayerPrefs.GetString("Scene"));
        if (PlayerPrefs.HasKey("HighestLevel"))
        {
            if (level > PlayerPrefs.GetInt("HighestLevel"))
            {
                PlayerPrefs.SetInt("HighestLevel", level);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighestLevel", level);
        }

        m_winCanvas.SetActive(true);
        if (m_winAudioPlayed) return;
        GameOverAudio(true);
    }

    /// <summary>
    /// 播放游戏结束时的音频
    /// </summary>
    public void GameOverAudio(bool isWin)
    {
        m_winAudioPlayed = isWin;
        m_soundTransform.GetComponent<AudioSource>().PlayOneShot(isWin ? m_winAudio : m_deathAudio);
    }
}