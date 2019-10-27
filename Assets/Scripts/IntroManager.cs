using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Transform[] m_canvas;
    public int m_currentIndex = 0;
    public Transform m_advanceButton;
    public Transform m_retreateButton;

    // Update is called once per frame
    void Update()
    {
        if (m_currentIndex == 0)
        {
            m_retreateButton.gameObject.SetActive(false);
        }
        else if (m_currentIndex == m_canvas.Length - 1)
        {
            m_advanceButton.gameObject.SetActive(false);
        }
        else
        {
            m_retreateButton.gameObject.SetActive(true);
            m_advanceButton.gameObject.SetActive(true);
        }
    }
    public void Forward()
    {
        m_currentIndex++;
        ShowCanvas(m_currentIndex);
    }
    public void Backward()
    {
        m_currentIndex--;
        ShowCanvas(m_currentIndex);
    }
    public void GoHome()
    {
        SceneManager.LoadScene("start");
    }

    private void ShowCanvas(int currentIndex)
    {
        for (var i = 0; i < m_canvas.Length; i++)
        {
            m_canvas[i].gameObject.SetActive(currentIndex == i);
        }

    }
}
