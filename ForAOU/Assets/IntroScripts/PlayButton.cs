using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void PlayGameStart()
    {
        Color color = image.color;

        if (color.a > 0)
        {
            color.a -= Time.deltaTime;
        }

        image.color = color;

        GameManager.instance.LoadScene(0);

        if (color.a < 1)
        {
            color.a += Time.deltaTime;
        }

        image.color = color;
    }
}
