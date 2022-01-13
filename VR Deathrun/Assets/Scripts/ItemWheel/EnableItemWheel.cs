using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class EnableItemWheel : MonoBehaviour
{
    //public InputActionProperty OpenWheelButton;
    //public InputActionProperty CloseWheelButton;
    [SerializeField]
    public GameObject itemWheel;

    public void WheelOpen()
    {
        itemWheel.SetActive(true);
    }
    public void WheelClose()
    {
        itemWheel.SetActive(false);
    }
}
