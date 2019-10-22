using UnityEngine;

public class WinCondiction
{
    public static bool m_isWin;
    public static WinCondiction Instant;

    public GameObject m_winCanvas;
    public Transform m_SoundTransform;
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
        if (m_winAudioPlayed != false) return;
        var audio = m_SoundTransform.GetComponent<AudioSource>();
        audio.PlayOneShot(m_winAudio);
        m_winAudioPlayed = true;
    }

    public void Death()
    {
        var audio = m_SoundTransform.GetComponent<AudioSource>();
        audio.PlayOneShot(m_deathAudio);
    }
}