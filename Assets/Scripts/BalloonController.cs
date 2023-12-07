using UnityEngine;

namespace Assets.Scripts
{
    public class BalloonController : MonoBehaviour
    {
        public PositionController Position;

        private Vector3? _startPosition;

        private bool Moving => Position.enabled;

        public void Move(float step)
        {
            if (Moving) return;

            _startPosition ??= transform.position;

            Position.To = transform.localPosition + new Vector3(step, 0);
            Position.enabled = true;
        }

        public void Reset()
        {
            transform.position = _startPosition ?? transform.position;
            Position.From = Vector2.zero;
        }
    }
}
