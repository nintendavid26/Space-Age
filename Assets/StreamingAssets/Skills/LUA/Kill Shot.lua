--Kil Shot
function Use()

    Controller.AnimationPlaying=true
    Target=Targets[0]
    User.PlaySound("Bolt",true,0.1)
    User.transform.LookAt(Target.transform)
    Lightning=Prefabs.MakeBolt(User.transform.position,Target.transform.position)
    while Lightning.Moving do
       coroutine.yield("WaitForEndOfFrame")
    end
    User.transform.eulerAngles = User.DefaultRot
    dmg = Skill.CalculateDamage(User,Target)
    Target.TakeDamage(dmg)
    if Target.stats["Health"]<=0 then
        PlayerShip.Money+=Target.Level*10
        User.Debug("Kill Shot")
    end
    Target.MakeExplosion()
     Controller.AnimationPlaying = false;

end