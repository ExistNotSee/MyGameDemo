using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevelManager : MonoBehaviour
{
    public int maxHighLevel;
    private int nowIndex = 1;
    public GameObject advanceButton;
    public GameObject retreatButton;
    public Text levelText;

    void Start()
    {
        maxHighLevel = PlayerPrefs.HasKey("HighestLevel") ? PlayerPrefs.GetInt("HighestLevel") : maxHighLevel;
        nowIndex = maxHighLevel;
        levelText.text = "第" + nowIndex + "关";
    }

    void Update()
    {
        advanceButton.SetActive(maxHighLevel != 1 && nowIndex != maxHighLevel);
        retreatButton.SetActive(maxHighLevel != 1 && nowIndex != 1);
    }

    /// <summary>
    /// 应用选择
    /// </summary>
    public void Accept()
    {
        PlayerPrefs.SetString("Scene", nowIndex.ToString().PadLeft(2, '0'));
        GameManager.m_score = 0;
        SceneManager.LoadScene("LoadScene");
    }

    public void Previous()
    {
        if (nowIndex <= 1) return;
        nowIndex--;
        levelText.text = "第" + nowIndex + "关";
    }

    public void Next()
    {
        if (nowIndex >= maxHighLevel) return;
        nowIndex++;
        levelText.text = "第" + nowIndex + "关";
    }

    public void GoHome()
    {
        SceneManager.LoadScene("start");
    }
}