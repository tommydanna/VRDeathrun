using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


namespace Autohand
{
    public class ChangeUILayer : MonoBehaviour
    {
        static public Hand instance;

        public void Start()
        {
            StartCoroutine(ChangeLayer());
        }

        IEnumerator ChangeLayer() 
        {
            yield return new WaitForSeconds(0.5f);
            Hand.SetLayerRecursive(transform, LayerMask.NameToLayer("UI"));
        }
    }

}