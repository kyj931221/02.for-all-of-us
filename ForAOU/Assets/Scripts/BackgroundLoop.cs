using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;

    private void Awake()
    {
        BoxCollider2D backgroundCollider = GetComponent<BoxCollider2D>();
        width = backgroundCollider.size.x;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if(transform.position.x <= -width / 0.5f)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 3f, 0);
        transform.position = (Vector2) transform.position + offset;
    }
}
