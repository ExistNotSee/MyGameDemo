using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    public static GameManager gameManager;
    public static int m_score = 0;
    public static int m_hightScore = 0;
    public int m_eatedDot = 0;
    public static int m_maxLevel = 10;
    public static bool m_paused = false;
    public static bool m_muted = false;
    public static bool m_buttonMode;
    public static bool m_rockerMode;
    public Text m_scoreText;
    public Text m_hightScoreText;
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
        gameManager = this;
        if (GameObject.FindGameObjectWithTag("Restart"))
        {
            Ghost.restartGameObject = GameObject.FindGameObjectWithTag("Restart");
            Ghost.restartGameObject.SetActive(false);
        }
        if (m_hightScoreText != null)
        {
            m_hightScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
            m_hightScoreText.text = "最高分：" + m_hightScore;
        }
        if (m_backgroundMusic != null)
        {
            m_backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
        }
      

        //设置移动模式
        if (m_buttonModeToggle != null &&m_rockerModeToggle != null)
        {
            if (PlayerPrefs.HasKey("Mode"))//有预设
            {
                var mode = PlayerPrefs.GetInt("Mode");
                if (mode == 0)//预设为按键模式
                {
                    m_buttonMode = true;
                    m_rockerMode = false;
                    m_buttonModeToggle.isOn = true;
                    m_rockerModeToggle.isOn = false;
                }
                else if (mode == 1)//预设为摇杆模式
                {
                    m_buttonMode = false;
                    m_rockerMode = true;
                    m_buttonModeToggle.isOn = false;
                    m_rockerModeToggle.isOn = true;
                }
            }
            else//第一次进入游戏，默认为按键模式
            {
                m_buttonMode = true;
                m_rockerMode = false;
                m_buttonModeToggle.isOn = true;
                m_rockerModeToggle.isOn = false;
                PlayerPrefs.SetInt("Mode", 0);
            }
        }

        if (m_muted != true) return;
        m_muteButton.GetComponent<Image>().sprite = m_audioStartSprite;
        var sound = GameObject.Find("Sound").GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.mute = true;
        }
        if (m_backgroundMusic != null)
        {
            m_backgroundMusic.mute = true;
        }
    }
    void Update () {
        if(m_lifeText!=null)
        {
            m_lifeText.text = "X " + PacmanMove.m_life;
        }
    }
    void Restart () {
        SceneManager.LoadScene("ChooseLevel");
        PacmanMove.m_life = PacmanMove.m_maxlife; 
    }

    public void addScore (int score) {
        m_score += score;
        m_scoreText.text = "分数：" + m_score;
        if (m_score > m_hightScore)
        {
            m_hightScore = m_score;
        }
        m_scoreText.text = "分数：" + m_score;
        m_hightScoreText.text = "最高分：" + m_hightScore;
    }

    public void ghostRevenge (GameObject ghost) {
        StartCoroutine(Revenge(ghost));
    }

    private IEnumerator Revenge(GameObject ghost)
    {
        yield return new WaitForSeconds(10);
        ghost.transform.position = Vector2.zero;
        ghost.SetActive(true);
    }
    
    public void EatDot()
    {
        m_eatedDot++;          
    }

    public void Win()
    {
        var SceneName = PlayerPrefs.GetString("Scene");
        int sceneNum = int.Parse(SceneName);
        sceneNum++;
        if (sceneNum <= m_maxLevel)
        {
            PlayerPrefs.SetString("Scene", sceneNum.ToString().PadLeft(2, '0'));
            SceneManager.LoadScene("Load");
        }
        else
        {
            SceneManager.LoadScene("Win");
        }
    }

    public void Pause()
    {
        if (m_paused == false)//暂停
        {
            m_pauseButton.GetComponent<Image>().sprite = m_startSprite;
            m_paused = true;
        }
        else//启动
        {
            m_pauseButton.GetComponent<Image>().sprite = m_pauseSprite;
            m_paused = false;
        }
    }

    public void Mute()
    {
        if (m_muted == false)//静音
        {
            m_muteButton.GetComponent<Image>().sprite = m_audioStartSprite;
            var sound = GameObject.Find("Sound").GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.mute = true;
            }
            if (m_backgroundMusic != null)
            {
                m_backgroundMusic.mute = true;
            }
            m_muted = true;
        }
        else//放音
        {
            m_muteButton.GetComponent<Image>().sprite = m_muteSprite;
            var sound = GameObject.Find("Sound").GetComponent<AudioSource>();
            if (sound != null)
            {
                sound.mute = false;
            }
            if (m_backgroundMusic != null)
            {
                m_backgroundMusic.mute = false;
            }
            m_muted = false;
        }
    }
    public void OnButtonToggleStateChanged()
    {
        if (m_buttonModeToggle.isOn)
        {
            m_rockerModeToggle.isOn = false;
            m_rockerMode = false;
            m_buttonMode = true;
            PlayerPrefs.SetInt("Mode", 0);//0为按键模式，1为摇杆模式
        }
        else
        {
            m_rockerModeToggle.isOn = true;
            m_rockerMode = true;
            m_buttonMode = false;
            PlayerPrefs.SetInt("Mode", 1);//0为按键模式，1为摇杆模式
        }
    }

    public void OnRockerToggleStateChanged()
    {
        if (m_rockerModeToggle.isOn)
        {
            m_buttonModeToggle.isOn = false;
            m_rockerMode = true;
            m_buttonMode = false;
            PlayerPrefs.SetInt("Mode", 1);//0为按键模式，1为摇杆模式
        }
        else
        {
            m_buttonModeToggle.isOn = true;
            m_rockerMode = false;
            m_buttonMode = true;
            PlayerPrefs.SetInt("Mode", 0);//0为按键模式，1为摇杆模式
        }
    }

    public void OnSetting()
    {
        if (m_settingGameObject.activeSelf == false)
        {
            m_settingGameObject.SetActive(true);
        }
        else
        {
            m_settingGameObject.SetActive(false);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("ChooseLevel");
        PacmanMove.m_life = PacmanMove.m_maxlife;
    }

    public void GoIntroductionScene()
    {
        SceneManager.LoadScene("Introduction");
    }
    public void SaveHighScore()
    {
        int oldHigh;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            oldHigh = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            oldHigh = 0;
        }
        if (m_hightScore > oldHigh)
        {
            PlayerPrefs.SetInt("HighScore",m_hightScore);
        }
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