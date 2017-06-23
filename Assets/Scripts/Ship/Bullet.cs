using UnityEngine;
using System.Collections;

namespace Overworld
{
    public class Bullet : MonoBehaviour
    {

        public ShipMovement ShotFrom;
        public float Speed;
        public float Lifetime;

        void Destroy()
        {
            Destroy(gameObject);
        }

        // Use this for initialization
        void Start()
        {
            Invoke("Destroy", Lifetime);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        }
    }
}