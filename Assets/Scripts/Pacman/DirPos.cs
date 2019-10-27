using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirPos : MonoBehaviour
{
    public Transform m_dirPos;
    public float m_distance;
    private Animator m_ani;

    // Start is called before the first frame update
    void Start()
    {
        m_ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //读取当前动画状态
        var stateInfo = m_ani.GetCurrentAnimatorStateInfo(0);
        //如果吃豆人面向右侧
        if (m_ani.IsInTransition(0)) return;
        var position = transform.position;
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.pacman_right"))
        {
            m_dirPos.position = new Vector2(position.x + m_distance, position.y); //方向点向右移动    
        }

        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.pacman_left"))
        {
            m_dirPos.position = new Vector2(position.x - m_distance, position.y); //方向点向左移动
        }

        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.pacman_up"))
        {
            m_dirPos.position = new Vector2(position.x, position.y + m_distance); //方向点向上移动
        }

        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.pacman_down"))
        {
            m_dirPos.position = new Vector2(position.x, position.y - m_distance); //方向点向下移动
        }
    }
}