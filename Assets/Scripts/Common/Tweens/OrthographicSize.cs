using UnityEngine;

namespace Assets.Scripts.Common.Tweens
{
    public class OrthographicSize : MonoBehaviour
    {
        public UnityEngine.Camera Camera;

        private float _target;
        private float _step;

        public void Tween(float target, float time)
        {
            _target = target;
            _step = (target - Camera.orthographicSize) / time * Time.deltaTime;
            enabled = true;
        }

        public void Update()
        {
            if (_step <= 0)
            {
                enabled = false;
            }

            Camera.orthographicSize += _step;

            if ((_step > 0 && Camera.orthographicSize >=_target) || (_step < 0 && Camera.orthographicSize <= _target))
            {
                Camera.orthographicSize = _target;
                enabled = false;
            }
        }
    }
}