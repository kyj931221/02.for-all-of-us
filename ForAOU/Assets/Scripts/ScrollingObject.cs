using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 200f;

    void Start()
    {
        
    }

   
    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
