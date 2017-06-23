using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public interface Status
    {
        void OnGain(Ship s);

        void OnCure(Ship s);

        string Name();

    }
}