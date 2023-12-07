using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    public AudioSource MenuMusic;

    public void Start()
    {
        if (PlayerPrefs.GetFloat("MusicVolume") == 0f)
        {
            this.GetComponent<Selectable>().OnSelect(null);
        }
    }

    public void OnClick()
    {
        if (MenuMusic.volume > 0f)
        {
            MenuMusic.volume = 0f;
            PlayerPrefs.SetFloat("MusicVolume", 0f);
            this.GetComponent<Selectable>().OnSelect(null);
        }
        else
        {
            MenuMusic.volume = 0.8f;
            PlayerPrefs.SetFloat("MusicVolume", 0.8f);
            this.GetComponent<Selectable>().OnDeselect(null);
        }
    }
}
