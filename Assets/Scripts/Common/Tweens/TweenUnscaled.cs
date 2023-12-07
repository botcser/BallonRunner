using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common.Tweens
{
    public class TweenUnscaled : MonoBehaviour
    {
        public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
        public Action OnComplete;

        private float _timeout;
        private Action _update;
        private Action _complete;
        private int _type;
        
        public static TweenUnscaled Color(Graphic target, Color from, Color to, float duration)
        {
            target.color = from;

            var component = target.GetComponents<TweenUnscaled>().SingleOrDefault(i => i._type == 0) ?? target.gameObject.AddComponent<TweenUnscaled>();
            var time = Time.unscaledTime;

            component._update = () => { target.color = from + (to - from) * component.AnimationCurve.Evaluate((Time.unscaledTime - time) / duration); };
            component._complete = () => { target.color = to; };
            component._timeout = Time.unscaledTime + duration;
            component._type = 0;
            component.enabled = true;
            component.OnComplete = null;

            return component;
        }

        public static TweenUnscaled Color(Graphic target, Color to, float duration)
        {
            return Color(target, target.color, to, duration);
        }

        public static TweenUnscaled Alpha(Graphic target, float from, float to, float duration)
        {
            return Color(target, new Color(target.color.r, target.color.g, target.color.b, from), new Color(target.color.r, target.color.g, target.color.b, to), duration);
        }

        public static TweenUnscaled Alpha(Graphic target, float to, float duration)
        {
            return Alpha(target, target.color.a, to, duration);
        }

        public static TweenUnscaled Color(SpriteRenderer target, Color from, Color to, float duration)
        {
            target.color = from;

            var component = target.GetComponent<TweenUnscaled>() ?? target.gameObject.AddComponent<TweenUnscaled>();
            var time = Time.unscaledTime;

            component._update = () => { target.color = from + (to - from) * component.AnimationCurve.Evaluate((Time.unscaledTime - time) / duration); };
            component._complete = () => { target.color = to; };
            component._timeout = Time.unscaledTime + duration;
            component.enabled = true;
            component.OnComplete = null;

            return component;
        }

        public static TweenUnscaled Color(SpriteRenderer target, Color to, float duration)
        {
            return Color(target, target.color, to, duration);
        }

        public static TweenUnscaled Alpha(SpriteRenderer target, float from, float to, float duration)
        {
            return Color(target, new Color(target.color.r, target.color.g, target.color.b, from), new Color(target.color.r, target.color.g, target.color.b, to), duration);
        }

        public static TweenUnscaled Alpha(SpriteRenderer target, float to, float duration)
        {
            return Alpha(target, target.color.a, to, duration);
        }

        public static TweenUnscaled Alpha(CanvasGroup target, float from, float to, float duration)
        {
            target.alpha = from;

            var component = target.GetComponents<TweenUnscaled>().SingleOrDefault(i => i._type == 1) ?? target.gameObject.AddComponent<TweenUnscaled>();
            var time = Time.unscaledTime;

            component._update = () => { target.alpha = from + (to - from) * component.AnimationCurve.Evaluate((Time.unscaledTime - time) / duration); };
            component._complete = () => { target.alpha = to; };
            component._timeout = Time.unscaledTime + duration;
            component._type = 1;
            component.enabled = true;
            component.OnComplete = null;

            return component;
        }

        public static TweenUnscaled Alpha(CanvasGroup target, float to, float duration)
        {
            return Alpha(target, target.alpha, to, duration);
        }

        public static TweenUnscaled Position(Transform target, Vector3 from, Vector3 to, float duration, bool local = true)
        {
            if (local)
            {
                target.localPosition = from;
            }
            else
            {
                target.position = from;
            }

            var component = target.GetComponents<TweenUnscaled>().SingleOrDefault(i => i._type == 2) ?? target.gameObject.AddComponent<TweenUnscaled>();
            var time = Time.unscaledTime;

            if (local)
            {
                component._update = () => { target.localPosition = from + (to - from) * component.AnimationCurve.Evaluate((Time.unscaledTime - time) / duration); };
                component._complete = () => { target.localPosition = to; };
            }
            else
            {
                component._update = () => { target.position = from + (to - from) * component.AnimationCurve.Evaluate((Time.unscaledTime - time) / duration); };
                component._complete = () => { target.position = to; };
            }

            component._timeout = Time.unscaledTime + duration;
            component._type = 2;
            component.enabled = true;
            component.OnComplete = null;

            return component;
        }

        public static TweenUnscaled Position(Transform target, Vector3 to, float duration, bool local = true)
        {
            return Position(target, target.localPosition, to, duration);
        }

        public static TweenUnscaled Scale(Transform target, Vector3 from, Vector3 to, float duration)
        {
            target.localScale = from;

            var component = target.GetComponents<TweenUnscaled>().SingleOrDefault(i => i._type == 3) ?? target.gameObject.AddComponent<TweenUnscaled>();
            var time = Time.unscaledTime;

            component._update = () => { target.localScale = from + (to - from) * component.AnimationCurve.Evaluate((Time.unscaledTime - time) / duration); };
            component._complete = () => { target.localScale = to; };

            component._timeout = Time.unscaledTime + duration;
            component._type = 3;
            component.enabled = true;
            component.OnComplete = null;

            return component;
        }

        public static TweenUnscaled Scale(Transform target, Vector3 to, float duration, bool local = true)
        {
            return Scale(target, target.localScale, to, duration);
        }

        public static TweenUnscaled Scale(Transform target, float to, float duration, bool local = true)
        {
            return Scale(target, target.localScale, to * Vector3.one, duration);
        }

        public static TweenUnscaled Scale(Transform target, float from, float to, float duration, bool local = true)
        {
            return Scale(target, from * Vector3.one, to * Vector3.one, duration);
        }

        public static TweenUnscaled FillAmount(Image target, float from, float to, float duration)
        {
            target.fillAmount = from;

            var component = target.GetComponents<TweenUnscaled>().SingleOrDefault(i => i._type == 4) ?? target.gameObject.AddComponent<TweenUnscaled>();
            var time = Time.unscaledTime;

            component._update = () => { target.fillAmount = from + (to - from) * component.AnimationCurve.Evaluate((Time.unscaledTime - time) / duration); };
            component._complete = () => { target.fillAmount = to; };

            component._timeout = Time.unscaledTime + duration;
            component._type = 4;
            component.enabled = true;
            component.OnComplete = null;

            return component;
        }

        public static TweenUnscaled FillAmount(Image target, float to, float duration, bool local = true)
        {
            return FillAmount(target, target.fillAmount, to, duration);
        }

        public static TweenUnscaled Volume(AudioSource target, float from, float to, float duration)
        {
            target.volume = from;

            var component = target.GetComponents<TweenUnscaled>().SingleOrDefault(i => i._type == 5) ?? target.gameObject.AddComponent<TweenUnscaled>();
            var time = Time.unscaledTime;

            component._update = () => { target.volume = from + (to - from) * component.AnimationCurve.Evaluate((Time.unscaledTime - time) / duration); };
            component._complete = () => { target.volume = to; };

            component._timeout = Time.unscaledTime + duration;
            component._type = 5;
            component.enabled = true;
            component.OnComplete = null;

            return component;
        }

        public static TweenUnscaled Volume(AudioSource target, float to, float duration, bool local = true)
        {
            return Volume(target, target.volume, to, duration);
        }

        public void Update()
        {
            if (Time.unscaledTime < _timeout)
            {
                _update();
            }
            else
            {
                _complete();
                _type = -1;
                Destroy(this);

                if (OnComplete != null)
                {
                    OnComplete();
                }
            }
        }
    }
}