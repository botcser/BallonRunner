using Assets.ECS.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.ECS.Systems
{
    public class Swipe : IEcsRunSystem
    {
        private EcsFilter<EntityData, IsPlayer> entityIds;

        private Vector2? _buttonDownPosition;
        private Vector2? _buttonUpPosition;

        public void Run()
        {
            if (_buttonDownPosition == null && Input.GetMouseButtonDown((int)MouseButton.LeftMouse) && Input.touchCount < 2)
            {
                _buttonDownPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp((int)MouseButton.LeftMouse) && _buttonDownPosition != null)
            {
                _buttonUpPosition = Input.mousePosition;

                SwipePlayer();
            }
            else if (Input.touchCount > 1)
            {
                _buttonDownPosition = null;
            }
        }

        private void SwipePlayer()
        {
            foreach (var entityId in entityIds)
            {
                ref var entity = ref entityIds.Get1(entityId);
                
                if (entity.Moving)
                {
                    _buttonDownPosition = _buttonUpPosition = null;
                }
                else if (_buttonDownPosition != null && _buttonUpPosition != null)
                {
                    var deltaPosition = _buttonDownPosition.GetValueOrDefault().x < _buttonUpPosition.GetValueOrDefault().x ? EcsGame.Instance.LineStep : -EcsGame.Instance.LineStep;

                    if (ValidatePosition(entity, deltaPosition))
                    {
                        InitPosition(ref entity, deltaPosition);
                    }

                    _buttonDownPosition = _buttonUpPosition = null;
                }

                break;
            }
        }

        private bool ValidatePosition(EntityData entity, float deltaPosition)
        {
            var endX = entity.Transform.anchoredPosition.x + deltaPosition;

            return endX < Game.Instance.Canvas.rect.width / 2 - 10 && endX > -Game.Instance.Canvas.rect.width / 2 + 10;
        }

        public void InitPosition(ref EntityData player, float deltaPosition)
        {
            player.From = player.Transform.anchoredPosition;
            player.To = player.Transform.anchoredPosition + new Vector2(deltaPosition, 0);
        }
    }
}
