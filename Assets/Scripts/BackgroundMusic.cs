using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource m_audioSource;
    public static int m_audioSize;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] bgm=  GameObject.FindGameObjectsWithTag("BackgroundMusic");
        if(bgm.Length>1){
            Destroy(gameObject);
        }
        if(bgm.Length==1){
            DontDestroyOnLoad(gameObject);
        }
        if(PlayerPrefs.HasKey("AudioSize")){
            m_audioSize=PlayerPrefs.GetInt("AudioSize");
        }else{
            m_audioSize=100;
        }
        m_audioSource=GetComponent<AudioSource>();
        m_audioSource.volume=m_audioSize/100;
    }

    // Update is called once per frame
    void Update()
    {
        m_audioSource.volume=m_audioSize/100;        
    }
}
