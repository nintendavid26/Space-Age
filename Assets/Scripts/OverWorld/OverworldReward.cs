using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{
    public class OverworldReward : MonoBehaviour
    {

        public enum Type { HP,Fuel,Money};
        public Type type;
        public int amnt;

        void Update()
        {
            transform.Rotate(Vector3.up*Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision with " + other.name);
            if (other.CompareTag("Player"))
            {
                if (type == Type.HP)
                {
                    PlayerShipMovement.Player.ship.Heal(amnt);

                }
                else if (type == Type.Fuel)
                {
                    PlayerShipMovement.Player.ship.GetFuel(amnt);
                }
                else if (type == Type.Money)
                {
                    PlayerShipMovement.Player.Money+=amnt;
                }
                PlayerShipMovement.Player.UpdateUI();
                Destroy(gameObject);
            }

        }


    }
}