using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class LightningBolt : LightningBoltScript {

    public Vector3 Target;
    public Vector3 startPos;
    public float speed=1;
    public float WaitBeforeDestroying = 1f;
    public bool Moving=true;


    protected override void Update()
    {

        base.Update();
        Vector3 Desired = Vector3.MoveTowards(EndPosition, Target, Time.deltaTime * speed);
        EndPosition = Desired;
        if (EndPosition == Target)
        {
            
            WaitBeforeDestroying -= Time.deltaTime;
            if (WaitBeforeDestroying <= 0)
            {
                Moving = false;
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
        Moving = true;
    }

}
