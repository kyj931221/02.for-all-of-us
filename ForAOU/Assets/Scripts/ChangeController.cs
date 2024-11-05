using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeController : MonoBehaviour
{
    public GameObject Vtcontroller;

    public void Vtcont()
    {
        if(Vtcontroller.activeSelf)
        {
            Vtcontroller.SetActive(!Vtcontroller.activeSelf);
        }
        else
        {
            Vtcontroller.SetActive(!Vtcontroller.activeSelf);
        }
    }
    
}
