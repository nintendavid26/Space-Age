--Fire1
function Use()

    Controller.AnimationPlaying=true
    Target=Targets[0]
    User.transform.LookAt(Target.transform)
    Fire=Prefabs.Make("Fire",User.transform.position)
    while Fire.transform.position!=Target.transform.position do
       Fire.transform.position=Prefabs.MoveTowards(Fire.transform.position, Target.transform.position, Prefabs.deltaTime() * 20)
       coroutine.yield("WaitForEndOfFrame")
    end
    coroutine.yield("WaitForSeconds 1")
    User.transform.eulerAngles = User.DefaultRot
    dmg = Skill.CalculateDamage(User,Target)
    Target.TakeDamage(dmg)
     Controller.AnimationPlaying = false;
    
    
end