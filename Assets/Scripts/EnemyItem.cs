using Assets.ECS;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyItem : Item
    {
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Destroy")
            {
                EnemySpawner.Instance.ItemsPool.AddItem(this);
            }
            else if (collision.tag == "Player")
            {
                if (EcsGame.Instance != null)
                {
                    EcsGame.Instance.GameOver();
                }
                else if (Game.Instance != null)
                {
                    Game.Instance.GameOver();
                }
            }
        }
    }
}
