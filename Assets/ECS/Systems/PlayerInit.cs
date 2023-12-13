using Assets.ECS.Components;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ECS.Systems
{
    public class PlayerInit : IEcsInitSystem
    {
        private int _entitiesCount;
        private EcsWorld _ecsWorld;

        public void Init()
        {
            InitPlayer();
        }
        
        private void InitPlayer()
        {
            var player = _ecsWorld.NewEntity();
            ref var entity = ref player.Get<EntityData>();

            entity.Transform = EcsGame.Instance.BalloonRectTransform;
            entity.Amplitude = 1;
            entity.Speed = 1.4f;
            player.Get<IsPlayer>();

            _entitiesCount++;
        }
    }
}
