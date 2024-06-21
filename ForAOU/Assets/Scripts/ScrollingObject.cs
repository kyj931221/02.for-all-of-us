using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 20f;

    void Start()
    {
        
    }

   
    private void Update()
    {
        if(!GameManager.instance.isGameover)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
