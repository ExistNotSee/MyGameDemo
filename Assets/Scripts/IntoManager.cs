using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntoManager : MonoBehaviour
{
    public Transform[] m_canvas;
    public int m_currentIndex = 0;
    public Transform m_advanceButton;
    public Transform m_retreateButton;
    // Start is called before the first frame update
    void Start()
    {

    }

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
    public void forward()
    {
        m_currentIndex++;
        showCanvas(m_currentIndex);
    }
    public void backward()
    {
        m_currentIndex--;
        showCanvas(m_currentIndex);
    }
    public void goHome()
    {
        SceneManager.LoadScene("start");
    }

    public void showCanvas(int currentIndex)
    {
        for (int i = 0; i < m_canvas.Length; i++)
        {
            if (currentIndex == i)
            {
                m_canvas[i].gameObject.SetActive(true);
            }
            else
            {
                m_canvas[i].gameObject.SetActive(false);
            }
        }

    }
}
