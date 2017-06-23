using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jammed : Status
{
    public string Name()
    {
        return "Jammed";
    }

    public void OnCure(Ship s)
    {
        throw new NotImplementedException();
    }

    public void OnGain(Ship s)
    {
        
    }
}
