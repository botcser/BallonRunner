using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common.Tweens;
using UnityEngine;

public class TweenScale : MonoBehaviour
{
    private Vector3 _scale;

    public void OnEnable()
    {
        var tween = Tween.Scale(transform, new Vector3(0, transform.localScale.y - 0.3f), _scale.y == 0 ? transform.localScale : _scale, 1);

        tween.OnComplete = () =>
        {
            if (_scale.y == 0)
            {
                _scale = transform.localScale;
            }
        };
    }

    public void OnDisable()
    {
        Tween.Scale(transform, transform.localScale, new Vector3(0, transform.localScale.y - 0.3f), 1);
    }
}
