using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Enum;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemsPool : MonoBehaviour
    {
        private readonly List<Item> _items = new();

        public void AddItem(Item item)
        {
            _items.Add(item);
            item.transform.SetParent(transform);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        public Item GetItem(ItemType type)
        {
            return _items.FirstOrDefault(i => i.Type == type);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
