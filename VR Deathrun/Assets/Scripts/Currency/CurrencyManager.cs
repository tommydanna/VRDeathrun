using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public TMP_Text currencyText;

    private void Awake()
    {
        PlayerPrefs.SetInt("currency", 3000);
        currencyText.text = PlayerPrefs.GetInt("currency").ToString("");
    }

    private void Update()
    {
        currencyText.text = PlayerPrefs.GetInt("currency").ToString("");
    }
}
