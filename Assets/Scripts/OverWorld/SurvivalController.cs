using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SurvivalController : MonoBehaviour {

    public static SurvivalController controller;

    public float WorldHeight;
    public float WorldWidth;
    public int PlayerMoney = 100;   

    void Awake()
    {
        controller = this;
    }


	
}
