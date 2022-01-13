using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CycleArray : MonoBehaviour
{
    public TMP_Text playerCountText;
    public TMP_Text roomTypeText;

    public int[] playerCount = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

    public string[] roomType = { "Private", "Public" };

    private int pcCurrentItem = 9;
    private int rtCurrentItem = 0;

    private void Start()
    {
        playerCountText.text = playerCount[pcCurrentItem].ToString();
        roomTypeText.text = roomType[rtCurrentItem];
    }

    public void PCNextButton()
    {
        pcCurrentItem++;

        if(pcCurrentItem > playerCount.Length - 1)
        {
            pcCurrentItem = 0;
        }

        playerCountText.text = playerCount[pcCurrentItem].ToString();
        roomTypeText.text = roomType[rtCurrentItem];
    }

    public void PCBackButton()
    {
        pcCurrentItem--;

        if (pcCurrentItem < 0)
        {
            pcCurrentItem = playerCount.Length - 1;
        }

        playerCountText.text = playerCount[pcCurrentItem].ToString();
        roomTypeText.text = roomType[rtCurrentItem];
    }

    public void RTNextButton()
    {
        rtCurrentItem++;

        if (rtCurrentItem > roomType.Length - 1)
        {
            rtCurrentItem = 0;
        }

        playerCountText.text = playerCount[pcCurrentItem].ToString();
        roomTypeText.text = roomType[rtCurrentItem];
    }

    public void RTBackButton()
    {
        rtCurrentItem--;

        if (rtCurrentItem < 0)
        {
            rtCurrentItem = roomType.Length - 1;
        }

        playerCountText.text = playerCount[pcCurrentItem].ToString();
        roomTypeText.text = roomType[rtCurrentItem];
    }


}
