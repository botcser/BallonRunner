using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Common;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public ItemsPool ItemsPool;
        public RectTransform BackgroundRectTransform;
        public CancellationTokenSource CancellationTokenSource;

        public static EnemySpawner Instance;

        [SerializeField] private List<RectTransform> _spawnLinePoints;
    
        private readonly List<Item> _items = new();

        void Awake()
        {
            Instance = this;
        }

        public void Init(float lineStep)
        {
            CancellationTokenSource = new CancellationTokenSource();
            _spawnLinePoints[0].anchoredPosition = new Vector2(-(lineStep + 20), 0);
            _spawnLinePoints[2].anchoredPosition = new Vector3(lineStep + 20, 0);
        }

        public void Reset()
        {
            _items.ForEach(i => Destroy(i.gameObject));
            _items.Clear();
            ItemsPool.Clear();
            CancellationTokenSource?.Dispose();
        }

        public IEnumerator SpawnStaticItems(List<ItemProto> itemPrototypes, float delaySecs)
        {
            while (true)
            {
                if (CancellationTokenSource.IsCancellationRequested)
                {
                    StopAllCoroutines();
                    break;
                }

                SpawnItem(itemPrototypes.Random(), _spawnLinePoints);

                yield return new WaitForSeconds(delaySecs);
            }
        }

        private void SpawnItem(ItemProto randomProto, List<RectTransform> spawnLinePoints)
        {
            var freeItem = ItemsPool.GetItem(randomProto.Type);

            if (freeItem != null)
            {
                ItemsPool.RemoveItem(freeItem);
                freeItem.transform.position = spawnLinePoints.Random().position;
                freeItem.transform.SetParent(BackgroundRectTransform);
            }
            else
            {
                var prefab = Instantiate(randomProto.Prefab, spawnLinePoints.Random());

                if (randomProto.Enemy)
                {
                    prefab.AddComponent<EnemyItem>();
                }

                var item = prefab.GetComponent<Item>();

                item.Type = randomProto.Type;
                item.Enemy = randomProto.Enemy;
                item.transform.SetParent(BackgroundRectTransform);

                _items.Add(item);
            }
        }
    }
}
