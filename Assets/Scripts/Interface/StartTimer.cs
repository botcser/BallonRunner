using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interface;
using UnityEngine;
using UnityEngine.UI;


public class StartTimer : MonoBehaviour
{
    public BaseInterface Parent;

    public void Run(Action callback = null)
    {
        StartCoroutine(Timer(callback));
    }

    private IEnumerator Timer(Action callback = null)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            transform.GetChild(i).gameObject.SetActive(false);
        }

        callback?.Invoke();
        Parent.Close();
    }
}
