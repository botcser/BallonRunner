using Assets.Scripts.Common.Tweens;
using UnityEngine;

namespace Assets.Scripts
{
    public class PositionController : TweenBase
    {
        public Vector2 From;
        public Vector2 To;
        public float Accelerate;
    
        private float _amplitude = 1;
        private Vector2 _startPosition;
    
        protected override float Sin()
        {
            return (Mathf.Cos(Speed * _time + Period * Mathf.PI) + 1) / 2;
        }

        protected override void OnUpdate()
        {
            _amplitude = Mathf.Max(0, _amplitude - Accelerate * Time.deltaTime);
            transform.localPosition = To - (To - From) * Sin() * _amplitude;
        
            if (_amplitude <= 0 || Vector2.Distance(To, transform.localPosition) < 0.01f)
            {
                enabled = false;
            }
        }

        public override void OnEnable()
        {
            _startPosition = transform.localPosition;

            if (From == Vector2.zero) From = _startPosition;

            base.OnEnable();
            Reset();
        }

        public void OnDisable()
        {
            if (SaveState)
            {
                transform.localPosition = _startPosition;
            }
            else
            {
                From = transform.localPosition;
            }
        }

        public void Reset()
        {
            _amplitude = 1;
        }
    }
}
