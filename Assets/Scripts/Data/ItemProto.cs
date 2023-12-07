using Assets.Scripts.Enum;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "Items", menuName = "Items/StaticItem")]
    public class ItemProto : ScriptableObject
    {
        public ItemType Type;
        public GameObject Prefab;
        public bool Enemy;
    }
}
