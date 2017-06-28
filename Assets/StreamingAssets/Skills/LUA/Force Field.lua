--Force Field
function Use()
    --TODO Destroy Charge
    Controller.AnimationPlaying=true
    User.stats.AddBuff("Def",3,1)
    User.stats.AddBuff("Atk",-3,1)
    User.PlaySound("Buff",true,0.1)
    Controller.AnimationPlaying=false
end