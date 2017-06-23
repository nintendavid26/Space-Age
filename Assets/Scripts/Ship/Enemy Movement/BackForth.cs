using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{
    public class BackForth : MonoBehaviour, EnemyShipMovementType
    {
        public float Speed;
        public float TurnTime; //how long before it turns
        public float ShootTime;
        public bool Clockwise;
        public float TurnTimer; //goes down a little each frame
        public float ShootTimer;
        public void Move(EnemyShipMovement e)
        {
            if (TurnTimer <= 0)
            {
                e.dir = OverWorld.Opposite(e.dir);
                TurnTimer = TurnTime;
            }
            if (ShootTimer <= 0)
            {
                e.Shoot();
                ShootTimer = ShootTime;
            }
            e.Move(e.dir);
            TurnTimer -= Time.deltaTime;
            ShootTimer -= Time.deltaTime;

        }
    }
}