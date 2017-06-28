using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Battle
{

public class Attack : BattleCommand
{

    public override IEnumerator Do(Ship User, Ship[] Target)
    {
        BattleController.Controller.AnimationPlaying = true;
        User.PlaySound("Lazer",true,0.1f);
        Ship target = Target[0];
        Quaternion prevR=User.transform.rotation;
        User.transform.LookAt(target.transform);
        GameObject bullet=BattlePrefabs.p.Make("Bullet",User.transform.position,User.transform.rotation);
        GameObject.Destroy(bullet.GetComponent<Overworld.Bullet>());
        while (bullet.transform.position!=target.transform.position) {
                bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.transform.position, Time.deltaTime * 20);
                yield return new WaitForEndOfFrame();
        }
        User.transform.eulerAngles = User.DefaultRot;
        GameObject.Destroy(bullet);
        int dmg = CalculateDamage(User, target);
        target.TakeDamage(dmg);
        target.MakeExplosion();
        BattleController.Controller.AnimationPlaying = false;
    }

        public int CalculateDamage(Ship User, Ship Target)
        {
            int dmg = 0;
            dmg = 2 * (User.stats["Atk"].Modified + 1) - Target.stats["Def"].Modified;
            int netLuck = 1 + User.stats["Luck"].Modified - Target.stats["Luck"].Modified;
            netLuck = Mathf.Clamp(netLuck, 1, 95);//Should  be from 1 to 95
            double crit = UnityEngine.Random.Range(1, 100) <= netLuck ? 1.5 : 1;

            //dmg = (int)(dmg * crit);
            return dmg;
        }

        public override Ship[] GetTarget(Ship User)
    {
        throw new NotImplementedException();
    }

    public override Ship[] ValidTargets(Ship User)
    {
            return User.Enemies.ToList().Where(x=>x.Alive()).ToArray();
    }
}
}
