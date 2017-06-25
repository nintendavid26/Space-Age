using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class LightningBolt : LightningBoltScript {

    public Vector3 Target;
    public Vector3 startPos;
    public float speed=1;
    public float Wait = 0.25f;


    protected override void Update()
    {

        base.Update();
        if (Wait > 0)
        {
            Wait -= Time.deltaTime;
        }
        else {
            Vector3 Desired = Vector3.MoveTowards(EndPosition, Target, Time.deltaTime * speed);
            EndPosition = Desired;
            if (EndPosition == Target)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void Initialize(Vector3 startPos,Vector3 target)
    {
        StartPosition = startPos;
        EndPosition = startPos;
        Target = target;
        StartObject = null;
        EndObject = null;
        Debug.Log(Target);
        Debug.Log(StartPosition);
        Debug.Log(EndPosition);
    }

}
