using System.Linq;
using Assets.ECS.Data;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.ECS.Components
{
    public class Moving : IEcsRunSystem
    {
        private EcsFilter<EntityData> entityIds;
        
        public void Run()
        {
            foreach (var entityId in entityIds)
            {
                ref var entity = ref entityIds.Get1(entityId);
                
                if (entity.To != entity.From)
                {
                    Move(ref entity);
                }
            }
        }

        private float Sin(EntityData entityData)
        {
            return (Mathf.Cos(entityData.Speed * entityData.Time) + 1) / 2;
        }

        private void Move(ref EntityData entity)
        {
            if (!entity.Moving)
            {
                entity.Time = 0;
            }

            entity.Amplitude = Mathf.Max(0, entity.Amplitude - entity.Dumping * Time.deltaTime);
            entity.Transform.anchoredPosition = entity.To - (entity.To - entity.From) * Sin(entity) * entity.Amplitude;
            entity.Moving = true;
            
            if (entity.Amplitude <= 0 || entity.isArrive())
            {
                entity.From = entity.To;
                entity.Moving = false;
                entity.Amplitude = 1;
            }

            entity.Time += Time.deltaTime;
        }
    }
}
