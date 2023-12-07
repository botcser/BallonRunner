using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Common
{
    public static class Extensions
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)System.Enum.Parse(typeof(T), value);
        }

        public static bool IsEmpty(this string target)
        {
            return string.IsNullOrEmpty(target);
        }

        public static void Clear(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void ClearImmediately(this Transform transform)
        {
            while (transform.childCount > 0)
            {
                Object.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        public static void SetActive(this Component target, bool active)
        {
            target.gameObject.SetActive(active);
        }

        public static void SetParentActive(this Component target, bool active)
        {
            target.transform.parent.gameObject.SetActive(active);
        }

        /// <summary>
        /// Consider using CopyTo and CopyFrom to avoid memory allocations.
        /// </summary>
        public static Texture2D Copy(this Texture2D texture)
        {
            var copy = new Texture2D(texture.width, texture.height) { filterMode = texture.filterMode };

            copy.SetPixels(texture.GetPixels());
            copy.Apply();

            return copy;
        }

        public static void SetAlpha(this Image target, float alpha)
        {
            var color = target.color;

            color.a = alpha;
            target.color = color;
        }

        public static bool SimilarTo(this Color32 a, Color32 b, int tolerance)
        {
            return Math.Abs(a.r - b.r) <= tolerance &&
                   Math.Abs(a.g - b.g) <= tolerance &&
                   Math.Abs(a.b - b.b) <= tolerance &&
                   Math.Abs(a.a - b.a) <= tolerance;
        }

        public static Coroutine ExecuteInNextUpdate(this MonoBehaviour monoBehaviour, Action action)
        {
            IEnumerator ExecuteInNextUpdate()
            {
                yield return null;
                action();
            }

            return monoBehaviour.StartCoroutine(ExecuteInNextUpdate());
        }

        public static Coroutine ExecuteIn(this MonoBehaviour monoBehaviour, Action action, float seconds)
        {
            IEnumerator ExecuteInNextUpdate()
            {
                yield return new WaitForSeconds(seconds);
                action();
            }

            return monoBehaviour.StartCoroutine(ExecuteInNextUpdate());
        }

        public static void SetAlpha(this Graphic target, float alpha)
        {
            var color = target.color;

            color.a = alpha;

            target.color = color;
        }

        public static void SetAlpha(this SpriteRenderer target, float alpha)
        {
            var color = target.color;

            color.a = alpha;

            target.color = color;
        }

        public static void Shuffle<T>(this List<T> source)
        {
            var n = source.Count;

            while (n > 1)
            {
                n--;

                var k = CRandom.Next(n + 1);
                var value = source[k];

                source[k] = source[n];
                source[n] = value;
            }
        }

        public static T Random<T>(this T[] source)
        {
            return source[UnityEngine.Random.Range(0, source.Length)];
        }

        public static T Random<T>(this List<T> source)
        {
            return source[UnityEngine.Random.Range(0, source.Count)];
        }

        public static T Random<T>(this List<T> source, List<T> exclude)
        {
            return source.Where(i => !exclude.Contains(i)).ToList()[UnityEngine.Random.Range(0, source.Count)];
        }

        public static bool IsEmptyE(this string target)
        {
            return string.IsNullOrEmpty(target);
        }
    }
}