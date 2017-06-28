--Big Force Field
function Use()
    Controller.AnimationPlaying=true
    for i=0, #Targets-1 do
        Targets[i].stats.AddBuff("Def",3,3)
    end
    User.PlaySound("Buff",true,0.1)
    Controller.AnimationPlaying=false
end