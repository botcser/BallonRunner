using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common.Tweens
{
    public class GridSpring : TweenBase
    {
        public Vector2 From;
        public Vector2 To;
        public float Dumping;
        public GridLayoutGroup Grid;
        public bool Loop = true;

        private Vector2 _spacing;
        private float _amplitude = 1;

        public static void Begin(Component target, Vector2 from, Vector2 to, float speed, float dumping)
        {
            var component = target.GetComponent<GridSpring>() ?? target.gameObject.AddComponent<GridSpring>();

            component.From = from;
            component.To = to;
            component.Speed = speed;
            component.Dumping = dumping;
            component.enabled = true;

            component.Grid = target.GetComponent<GridLayoutGroup>();
            component.SetActive(component.Grid != null);
        }

        protected override float Sin()
        {
            return (Mathf.Cos(Speed * _time + Period * Mathf.PI) + 1) / 2;
        }

        protected override void OnUpdate()
        {
            if (Grid == null) return;

            _amplitude = Mathf.Max(0, _amplitude - Dumping * Time.deltaTime);
            
            Grid.spacing = From + (To - From) * Sin() * _amplitude;

            if (_amplitude <= 0 || !Loop && Vector2.Distance(To, Grid.spacing) < 0.01f)
            {
                enabled = false;
            }
        }

        public override void OnEnable()
        {
            if (Grid != null) _spacing = Grid.spacing;

            base.OnEnable();
            Reset();
        }

        public void OnDisable()
        {
            if (Grid != null && Loop) Grid.spacing = _spacing;
        }

        public void Reset()
        {
            _amplitude = 1;
        }
    }
}