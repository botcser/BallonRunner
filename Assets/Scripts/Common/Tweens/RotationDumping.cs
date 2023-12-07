using UnityEngine;

namespace Assets.Scripts.Common.Tweens
{
    public class RotationDumping : TweenBase
    {
        public new Axis Axis;
        public float Dumping;

        private float _speed;
        private float _angle;

        protected override void OnUpdate()
        {
            _speed = Speed > 0
                ? Mathf.Max(0, _speed - Dumping * Time.deltaTime)
                : Mathf.Min(0, _speed + Dumping * Time.deltaTime);
            
            _angle += _speed * Time.deltaTime;

            transform.localRotation = Quaternion.Euler((Axis == Axis.X ? 1 : 0) * _angle, (Axis == Axis.Y ? 1 : 0) * _angle, (Axis == Axis.Z ? 1 : 0) * _angle);
        }

        public void Stop()
        {
            _speed = 0;
        }

        public void SpeedUp(float add)
        {
            if (Speed > 0)
            {
                _speed = Mathf.Max(0, Mathf.Min(Speed, _speed + add));
            }
            else
            {
                _speed = Mathf.Min(0, Mathf.Max(Speed, _speed - add));
            }
        }
    }
}