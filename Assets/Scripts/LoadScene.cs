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
        // 上下左右，对应坐标系上下左右
//        print(Vector2.down);
//        print(Vector2.up);
//        print(Vector2.left);
//        print(Vector2.right);
// 
//        print(Vector2.one);
//        print(Vector2.zero);
// 
//        Vector2 a = new Vector2(2, 2);
//        Vector2 b = new Vector2(3, 4);
//        Vector2 c = new Vector2(3, 0);
//        print(a.magnitude);
//        print(a.sqrMagnitude);
//        print(b.magnitude);
//        print(b.sqrMagnitude);
// 
//        print(a.normalized);
// 
//        //向量是结构体，是值类型，要整体赋值
//        transform.position = new Vector3(3, 3, 3);
//        Vector3 pos = transform.position;
//        pos.x = 10;
//        transform.position = pos;
// 
//        // 两个向量夹角
//        print(Vector2.Angle(a, b));
//        print(Vector2.Angle(a, c));
//        // 返回一个限制在最大长度2的向量
//        print(Vector2.ClampMagnitude(c, 2));//(2,0)
//        print(Vector2.Distance(b, c));//4
//        // a,b向量之间插值
//        print(Vector2.Lerp(a, b, 0.5f));//(2.5, 3)
//        print(Vector2.LerpUnclamped(a, b, 0.5f));//(2.5, 3)
// 
//        print(Vector2.Lerp(a, b, 2f));// (3,4)
//        // 沿着b的方向，延伸2倍，在x方向b - a=1，在b的基础上加1 * 2倍；在y方向上b-a=2，在b的基础上加2 * 2倍
//        print(Vector2.LerpUnclamped(a, b, 3f));// (5,8)
//        // 返回最大长度的向量
//        print(Vector2.Max(a, b));
//        print(Vector2.Min(a, b));
//        StartCoroutine(Loading());
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