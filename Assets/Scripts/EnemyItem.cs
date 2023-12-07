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
                Game.Instance.GameOver();
            }
        }
    }
}
