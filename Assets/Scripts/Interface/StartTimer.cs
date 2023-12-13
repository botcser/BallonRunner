using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interface
{
    public class StartTimer : MonoBehaviour
    {
        public GameObject Timers;
        public GameObject Panel;
        
        public void Run(Action callback = null)
        {
            Panel.SetActive(true);
            StartCoroutine(Timer(callback));
        }
        
        private IEnumerator Timer(Action callback = null)
        {
            for (var i = 0; i < Timers.transform.childCount; i++)
            {
                Timers.transform.GetChild(i).gameObject.SetActive(true);

                yield return new WaitForSeconds(1f);

                Timers.transform.GetChild(i).gameObject.SetActive(false);
            }

            callback?.Invoke();
            Panel.SetActive(false);
        }
    }
}
