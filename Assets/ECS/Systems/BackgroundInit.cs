using Assets.ECS.Components;
using Assets.ECS.Data;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Leopotam.Ecs.EcsWorld;

namespace Assets.ECS.Systems
{
    public class BackgroundInit : IEcsInitSystem, IEcsRunSystem
    {
        private int _entitiesCount;
        private GameState _gameState;
        private EcsWorld _ecsWorld;
        private EcsFilter<EntityData, IsBackground> entityIds;

        public void Init()
        {
            InitBackground();
        }

        private void InitBackground()
        {
            var backgroundEntity = _ecsWorld.NewEntity();
            ref var backgroundEntityData = ref backgroundEntity.Get<EntityData>();

            backgroundEntityData.Transform = EcsGame.Instance.BackgroundRectTransform;
            backgroundEntityData.Amplitude = 1;
            backgroundEntityData.Speed = 0.1f;
            backgroundEntity.Get<IsBackground>();

            _entitiesCount++;
        }

        public void Run()
        {
            if (_gameState.Playing)
            {
                foreach (var entityId in entityIds)
                {
                    ref var entity = ref entityIds.Get1(entityId);

                    if (!entity.Moving)
                    {
                        entity.From = entity.Transform.anchoredPosition;
                        entity.To = new Vector2(0, -5366);
                    }

                    if (entity.isArrive())
                    {
                        EcsGame.Instance.GameSuccess();
                    }
                }
            }
        }
    }
}
