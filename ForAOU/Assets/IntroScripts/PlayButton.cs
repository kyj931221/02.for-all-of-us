using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public void PlayGameStart()
    {
        GameManager.instance.LoadScene(1);
    }
}
