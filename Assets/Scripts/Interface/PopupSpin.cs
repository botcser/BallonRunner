using System;
using System.Collections;
using System.Globalization;
using Assets.Scripts.Common.Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface
{
	public class PopupSpin : MonoBehaviour
	{
        public GameObject CanvasGroupGO;
		public CanvasGroup CanvasGroup;
		public Text Message;
		public GameObject LoadingButton;
		public GameObject CancelButton;
		public GameObject ConfirmButton;
		public GameObject RetryButton;
        public GameObject NavigateButton;
        public GameObject BreakButton;
		public InputField Count;
        public bool Break;

		private Action _cancel, _confirm, _retry, _navigate;
        private Action<int> _confirmVar;
		private string _message;
		public bool Opened => CanvasGroup.alpha > 0;
	    public bool Running => LoadingButton.activeSelf;

        public static PopupSpin Instance;

		public void Awake()
		{
			Instance = this;
		}

        public void BreakNow()
        {
            Break = true;
        }

		public void Run(Action action, string message, bool silent = false, bool breakable = false)
		{
            if (!silent)
            {
				BreakButton.SetActive(breakable);
                Break = false;
                Message.text = message;
                CanvasGroupGO.SetActive(true);
                CanvasGroup.blocksRaycasts = true;
                Tween.Alpha(CanvasGroup, CanvasGroup.alpha, 1, 0.25f);
                StartSpin();
                StartCoroutine(StartAction(action, 1));
            }
            else
            {
				CanvasGroupGO.SetActive(true);
                CanvasGroup.blocksRaycasts = true; 
                Tween.Alpha(CanvasGroup, CanvasGroup.alpha, 0.01f, 0.25f);
                StartCoroutine(StartAction(action, 0.5f)); 
                ConfirmButton.SetActive(false);
			}
        }
		
        public void RequestConfirmation(string pattern, Action cancel = null, Action confirm = null, Action navigate = null, params object[] args)
        {
            Message.text = _message = string.Format(pattern, args);
            CanvasGroupGO.SetActive(true);
			CanvasGroup.blocksRaycasts = true;
            Tween.Alpha(CanvasGroup, CanvasGroup.alpha, 1, 0.25f);

            _confirm = confirm;
            PrepareButtons(cancel, confirm, navigate);

            if (cancel != null || confirm != null || navigate != null) return;

            _cancel = () => { };
            CancelButton.SetActive(true);
        }

        public void Stop(string message, Action cancel = null, Action confirm = null, Action retry = null, Action navigate = null)
		{
			_confirm = confirm;
            Message.text = message;
            PrepareButtons(cancel, confirm, retry, navigate);

            if (message == null)
            {
				Close();

				return;
            }

            if (cancel != null || confirm != null || retry != null || navigate != null) return;
        }

        private void PrepareButtons(Action cancel = null, object confirm = null, Action retry = null, Action navigate = null)
        {
            _cancel = cancel;
            _retry = retry;
            _navigate = navigate;

            LoadingButton.SetActive(false);
            CancelButton.SetActive(cancel != null);
            ConfirmButton.SetActive(confirm != null);
            RetryButton.SetActive(retry != null);
        }

		public void OnBackgroundTap()
		{
			if (CancelButton.activeSelf)
			{
				Cancel();
			}
			else if (ConfirmButton.activeSelf)
			{
				Confirm();
			}
		}

		public void Close()
		{
			CanvasGroup.blocksRaycasts = false;
            Count.gameObject.SetActive(false);
			Tween.Alpha(CanvasGroup, CanvasGroup.alpha, 0, 0.25f);
            CanvasGroupGO.SetActive(false);
            CanvasGroup.alpha = 0;
		}

		public void Cancel()
		{
			Close();
			_cancel();
		}

		public void Confirm()
        {
			Close();
			
            if (_confirm == null)
			{
				_confirmVar(int.Parse(Count.text == "" ? "0" : Count.text, CultureInfo.InvariantCulture));
            }
            else
            {
                _confirm();
            }

            _confirm = null;
        }

        public void Retry()
		{
			StartSpin();
			Message.text = _message;
			_retry();
		}

		public void Navigate()
		{
			_navigate();
		}

		private void StartSpin()
		{
			LoadingButton.SetActive(true);
			CancelButton.SetActive(false);
			ConfirmButton.SetActive(false);
			RetryButton.SetActive(false);
		}

		private IEnumerator StartAction(Action action, float delay)
		{
			yield return new WaitForSeconds(delay);

			action?.Invoke();
		}
    }
}