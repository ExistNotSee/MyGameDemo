using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Transform m_pacman;
    public Transform m_startPos;
    public Transform m_endPos;
    public List<Transform> m_eated;
    public float m_speed = 0.1f;
    public bool m_loadEnd = false;

    void Start()
    {
        StartCoroutine(Loading());
    }

    void Update()
    {
        var p = Vector2.MoveTowards(m_pacman.position, m_endPos.position, m_speed);
        m_pacman.position = p;
        if (Vector2.Distance(m_pacman.position, m_endPos.position) >= 0.1f) return;
        m_pacman.position = m_startPos.position;
        foreach (var item in m_eated)
        {
            item.gameObject.SetActive(true);
        }

        m_loadEnd = true;
    }

    private IEnumerator Loading()
    {
//        var sceneName = PlayerPrefs.GetString("Scene");
//        print("sceneName:"+sceneName);
//        var op = SceneManager.LoadSceneAsync("01");
//        op.allowSceneActivation = false;
//        while (op.progress < 0.9f || !m_loadEnd)
//        {
            yield return new WaitForEndOfFrame();
//        }
//
//        op.allowSceneActivation = true;
    }
}