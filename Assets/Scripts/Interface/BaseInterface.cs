using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common.Tweens;
using Assets.Scripts.Interface.Elements;
using UnityEngine;

namespace Assets.Scripts.Interface
{
	public class BaseInterface : MonoBehaviour
	{
		public GameObject Panel;
        public Transform Bar;
		public bool Modal;
		public BaseInterface ReturnTo;

        private Vector3 _scale;

		public bool Opened => Panel.activeSelf;

		public static readonly List<BaseInterface> Current = new List<BaseInterface>();

		public void OnValidate()
		{
			Panel = Panel ?? transform.Find("Panel").gameObject;
		}

		public void Open()
		{
            if (Opened && Current.Contains(this)) return;

            if (Bar != null)
            {
                var tween = Tween.Scale(Bar, new Vector3(0, Bar.localScale.y - 0.3f), _scale.y == 0 ? Bar.localScale : _scale, 0.6f);

                tween.OnComplete = () =>
                {
                    if (_scale.y == 0)
                    {
                        _scale = Bar.localScale;
                    }
                };
            }

            Blackout.Instance.Show(() =>
			{
				Blackout.Instance.Hide();

				if (!Modal)
				{
					foreach (var window in Current.Where(i => !i.Modal).ToList())
					{
						window.Close(auto: true);
					}
				}

				OnOpen();
				Panel.SetActive(true);
				Current.Add(this);
			});
		}

		public void Close(bool auto = false)
		{
			if (!Opened) return;

            if (Bar != null)
            {
                Tween.Scale(Bar, Bar.localScale, new Vector3(0, Bar.localScale.y - 0.3f), 1);
                Panel.SetActive(false);
                Current.Remove(this);
                OnClose(auto);

				return;
			}

            Panel.SetActive(false);
            Current.Remove(this);
            OnClose(auto);
			//ReturnTo?.Open();
		}

		protected virtual void OnOpen()
		{
		}

		protected virtual void OnClose(bool auto)
		{
		}

		public void OnDestroy()
		{
			if (Current.Contains(this)) Current.Remove(this);
		}

		public void Navigate(string url)
		{
			Application.OpenURL(url);
		}
	}
}