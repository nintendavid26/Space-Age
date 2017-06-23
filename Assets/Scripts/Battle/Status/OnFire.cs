using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Battle
{
    [MoonSharpUserData]
    public class OnFire : Status
    {
        public string Name()
        {
            return "On Fire";
        }

        public void OnCure(Ship s)
        {
            throw new NotImplementedException();
        }

        public void OnGain(Ship s)
        {
            s.EndOfTurn += TakeDamage;
        }

        public IEnumerator TakeDamage(Ship S)
        {
            S.TakeDamage(S.stats.maxHealth / 10);
            yield return null;
        }

    }
}