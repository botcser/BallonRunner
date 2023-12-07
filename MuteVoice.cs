using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class MuteVoice : MonoBehaviour
    {
        public GameObject ChildButton;

        public void Start()
        {
            if (PlayerPrefs.GetInt("PlayersVoice") == 1f)
            {
                this.GetComponent<Selectable>().OnSelect(null);
                ChildButton.SetActive(false);
            }
        }

        public void OnClick()
        {
            if (PlayerPrefs.GetInt("PlayersVoice") == 0)
            {
                PlayerPrefs.SetInt("PlayersVoice", 1);           // 1 - выключено
                this.GetComponent<Selectable>().OnSelect(null);
                ChildButton.SetActive(false);
            }
            else
            {
                this.GetComponent<Selectable>().OnDeselect(null);
                ChildButton.SetActive(true);
                PlayerPrefs.SetInt("PlayersVoice", 0);
            }
        }
    }
}
