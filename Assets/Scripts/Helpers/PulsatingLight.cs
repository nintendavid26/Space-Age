using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class PulsatingLight : MonoBehaviour {

    public Light pulse;
    public float speed;
    public float maxDist,minDist;

    void Update()
    {
        pulse.range=Mathf.PingPong(Time.time * speed, maxDist-minDist)+minDist;
    }

}
