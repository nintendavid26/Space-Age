using Overworld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looping : MonoBehaviour {

    float WorldWidth;
    float WorldHeight;
    PlayerShipMovement player;

    void Start()
    {
        WorldWidth = SurvivalController.controller.WorldWidth;
        WorldHeight = SurvivalController.controller.WorldHeight;
        player = PlayerShipMovement.Player;
    }

    void Update()
    {
        if (player.transform.position.x - transform.position.x > WorldWidth / 2)
        {
            transform.position=new Vector3(transform.position.x + WorldWidth,transform.position.y,transform.position.z);
        }
        else if(transform.position.x - player.transform.position.x > WorldWidth/2)
        {
            transform.position = new Vector3(transform.position.x - WorldWidth, transform.position.y, transform.position.z);
        }
        if (player.transform.position.z - transform.position.z > WorldHeight / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+WorldHeight);
        }
        else if (transform.position.z - player.transform.position.z > WorldHeight/2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - WorldHeight);
        }
    }
	
}
