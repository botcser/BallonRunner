using System;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets.ECS.Components
{
    public struct EntityData
    {
        public RectTransform Transform;
        public Vector2 From;
        public Vector2 To;
        public bool Moving;
        public float Amplitude;
        public float Dumping;
        public float Time;
        public float Speed;
        public bool isArrive()
        {
            return Vector2.Distance(To, Transform.anchoredPosition) < 0.02f;
        }

        public void Reset()
        {
            Time = 0;
            Moving = false;
            To = From = Transform.anchoredPosition;
        }
    }

    public struct IsPlayer
    {

    }

    public struct IsBackground
    {

    }
}
