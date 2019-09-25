using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    public static GameManager gameManager;
    public static int m_score = 0;
    public static int m_hightSocre = 0;
    public int m_eatedDot = 0;
    public static int m_maxLevel = 10;
    public static bool m_paused = false;
    public static bool m_muted = false;
    public static bool m_buttonMode;
    public static bool m_rockerMode;
    public Text m_scoreText;
    public Text m_hightSocreText;
    public Text m_lifeText;
    public Transform m_pauseButton;
    public Sprite m_startSprite;
    public Sprite m_pauseSprite;
    public Transform m_muteButton;
    public Sprite m_muteSprite;
    public Sprite m_audioStartSprite;
    public AudioSource m_backgroundMusic;
    public Toggle m_buttonModeToggle;
    public Toggle m_rockerModeToggle;
    public GameObject m_settingGameObject;

    void Start () {

    }
    void Update () {

    }
    void Restart () {

    }
    void addScore (int score) {

    }
    void ghostRevenge (GameObject ghost) {

    }
    public void clickSetting () {
        if (!m_settingGameObject.activeSelf) {
            m_settingGameObject.SetActive (true);
        } else {
            m_settingGameObject.SetActive (false);
        }
    }
    public void clickStartGame () {

    }
    public void clickInfoBtn () {
        SceneManager.LoadScene ("Introduction");
    }
    public void quitGame () {
        Application.Quit ();
    }
}