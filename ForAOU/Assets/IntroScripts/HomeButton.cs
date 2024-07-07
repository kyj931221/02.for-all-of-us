using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public void PlayGameHome()
    {
        GameManager.instance.LoadScene(0);
    }
}
