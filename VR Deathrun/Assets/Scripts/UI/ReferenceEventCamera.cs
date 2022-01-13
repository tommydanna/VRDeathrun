using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceEventCamera : MonoBehaviour
{
    public Canvas canvas;

    private void Start()
    {
        StartCoroutine(GetCam());
    }

    IEnumerator GetCam() 
    {
        yield return new WaitForSeconds(1);
        canvas.worldCamera = GameObject.Find("Camera (head)").GetComponent<Camera>();
    }
}
