using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ItemWheel : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionProperty OpenWheelButton;

    [SerializeField]
    private GameObject rightHandWand;
    [SerializeField]
    private GameObject rightHandBook;
    [SerializeField]
    private GameObject righthand;

    private void OnTriggerEnter(Collider other)
    {
        var key = other.GetComponentInChildren<TextMeshPro>();

        if (OpenWheelButton.action.enabled) 
        {
            if (key != null)
            {
                if (key.text == "WAND")
                {
                    rightHandWand.SetActive(true);
                    rightHandBook.SetActive(false);
                    righthand.SetActive(false);
                }
                else if (key.text == "BOOK")
                {
                    rightHandWand.SetActive(false);
                    rightHandBook.SetActive(true);
                    righthand.SetActive(false);
                }
                else if (key.text == "HANDS")
                {
                    rightHandWand.SetActive(false);
                    rightHandBook.SetActive(false);
                    righthand.SetActive(true);
                }
            }
        } 
    }
}
