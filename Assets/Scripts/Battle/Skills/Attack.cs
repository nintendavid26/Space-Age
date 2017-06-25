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
        int dmg = User.stats["Atk",true]-target.stats["Def",true];
        target.TakeDamage(User.stats["Atk",true]);
        target.MakeExplosion();
        BattleController.Controller.AnimationPlaying = false;
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
