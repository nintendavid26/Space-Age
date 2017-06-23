using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

namespace Overworld
{
    public enum Direction { N,NE,E,SE,S,SW,W,NW}
    public class ShipMovement : MonoBehaviour
    {

        const float SQRT2 = 1.4f;

        public bool ShipCanMove;
        public Ship ship;
        public float tilt;
        public float movementSpeed;
        public Bullet bullet;
        public Rigidbody rb;
        public Direction dir;

        public void Start()
        {
            ship = GetComponent<Ship>();
        }

        public virtual void Move(Direction dir)
        {
            if (dir == Direction.N)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (dir == Direction.S)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (dir == Direction.W)
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (dir == Direction.E)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (dir == Direction.NE)
            {
                transform.eulerAngles = new Vector3(0, 45, 0);
            }
            else if (dir == Direction.NW)
            {
                transform.eulerAngles = new Vector3(0, 315, 0);
            }
            else if (dir == Direction.SE)
            {
                transform.eulerAngles = new Vector3(0, 135, 0);
            }
            else if (dir == Direction.SW)
            {
                transform.eulerAngles = new Vector3(0, 225, 0);
            }
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            this.dir = dir;
        }

        public virtual void Shoot()
        {
            if (ship.stats.fuel == 0||!ShipCanMove) { return; }
            Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.ShotFrom = this;
            
        }
        
    }
}