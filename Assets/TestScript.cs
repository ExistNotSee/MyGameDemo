using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("-----------------");

//        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        GameObject gameObject = new GameObject();
        gameObject.transform.position = new Vector3(-15, -15, 0);
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>().mass = 0.5f;
        GameObject gameObject2 = MonoBehaviour.Instantiate(new GameObject());
        gameObject2.name = "1212";
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}