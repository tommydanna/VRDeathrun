using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyWand : MonoBehaviour
{
    public int wandPrice;

    public GameObject yesButton;

    public string playerPrefName;


    // un comment start func to reset locked state
    //private void Start()
    //{
    //    PlayerPrefs.SetInt(playerPrefName, 1);
    //}

    public void Buy() 
    {
        if (PlayerPrefs.GetInt("currency") >= wandPrice) 
        {
            PlayerPrefs.SetInt("currency", PlayerPrefs.GetInt("currency") - wandPrice);

            if (playerPrefName != null) 
            {
                PlayerPrefs.SetInt(playerPrefName, 0);
            }       
        }  
    }

    public void CanBuyCheck() 
    {
        if (PlayerPrefs.GetInt("currency") < wandPrice)
        {
            yesButton.layer = LayerMask.NameToLayer("Default");
        }
        else if(PlayerPrefs.GetInt("currency") >= wandPrice) 
        {
            yesButton.layer = LayerMask.NameToLayer("UI");
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(playerPrefName) == 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
