using UnityEngine;
using System.Collections;

namespace Overworld
{
    public class OverWorld : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static Direction Rot(Direction D, bool Clockwise)
        {
            if (Clockwise)
            {
                if (D == Direction.NW) { return Direction.N; }
                return D + 1;
            }
            if (D == Direction.N) { return Direction.NW; }
            return D - 1;
        }
        public static Direction Opposite(Direction D)
        {
            if ((int)D < 4) { return D + 4; }
            return D-4;
        }
    }

}