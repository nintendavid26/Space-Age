using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions.Coroutines
{
    public static class Coroutines
    {
        /// <summary>
        /// Given a list of coroutines, wait for one to finish before starting the next.
        /// Call this with StartCoroutine(this.Sequence(params[])));
        /// </summary>
        /// <param name="g"></param>
        /// <param name="coroutines"></param>
        public static IEnumerator Sequence(this MonoBehaviour m, params IEnumerator[] coroutines)
        {
            Coroutine c= m.StartCoroutine(coroutines[0]);
            foreach (IEnumerator i in coroutines) {
                c= m.StartCoroutine(i);
                yield return c;
                
            }
            yield return c;
        }

    }
}
