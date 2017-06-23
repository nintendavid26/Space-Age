using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{
    public class Asteroid : MonoBehaviour
    {

        public OverworldReward Reward;
        public int amnt;
        bool Spawned = false;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet")&&!Spawned)
            {
                SpawnItem();
                Spawned = true;
                Destroy(other);
                Destroy(gameObject);
            }

        }

        void SpawnItem()
        {
            OverworldReward r= Instantiate(Reward,transform.position,Quaternion.identity);
            r.amnt = amnt;

        }
    }
}