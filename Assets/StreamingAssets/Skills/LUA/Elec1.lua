--Elec1
function Use()

    Controller.AnimationPlaying=true
    Target=Targets[0]
    User.transform.LookAt(Target.transform)
    Lightning=Prefabs.Make("Lightning",User.transform.position)
    Lightning.Target=Target.transform.position
    while Lightning.EndPosition!=Lightning.Target do
       coroutine.yield("WaitForEndOfFrame")
    end
    coroutine.yield("WaitForSeconds 1")
    User.transform.eulerAngles = User.DefaultRot
    dmg = Skill.CalculateDamage(User,Target)
    Target.TakeDamage(dmg)
     Controller.AnimationPlaying = false;

end