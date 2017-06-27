using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using Battle;
using System.IO;
using System;

public static class SkillParser {
    static string path = "/Skills/LUA/";
    public static MonoBehaviour C=new MonoBehaviour();
    public static IEnumerator UseEffect(BattleSkill skill, string FName, Ship User, Ship[] Targets)
    {
        if (!ContainsFunction(FName, skill.Name)) { yield return null; }
        Script script = Parse(skill, User, Targets);
        DynValue coroutine = script.CreateCoroutine(script.Globals[FName]);
        DynValue x;
        x = coroutine.Coroutine.Resume();
        yield return Wait(x);
        while (true)
        {

            if (x.ToString() != "void")
            {
                x = coroutine.Coroutine.Resume();
                yield return Wait(x);
            }
            else { yield return null; }

        }
    }

    public static bool ContainsFunction(string FName, string skill)
    {//There's probably a better way to do this
        return File.ReadAllText(Application.streamingAssetsPath + path + skill + ".lua").Contains(FName);
    }

    public static Script Parse(BattleSkill skill, Ship User,Ship[] Targets, Dictionary<string, object> vars = null)
    {
        UserData.RegisterAssembly();//Is it cleaner to put all types here, or at the start of each file?
        UserData.RegisterType<Ship>();
        UserData.RegisterType<Ship[]>();
        UserData.RegisterType<BattleSkill>();
        UserData.RegisterType<BattlePrefabs>();
        UserData.RegisterType<BattleController>();
        UserData.RegisterType<Transform>();
        UserData.RegisterType<Vector3>();
        UserData.RegisterType<GameObject>();
        UserData.RegisterType<Stats>();
        UserData.RegisterType<Time>();
        UserData.RegisterType<LightningBolt>();
        UserData.RegisterType<SoundEffects>();
        UserData.RegisterExtensionType(typeof(Audio));

        //UserData.RegisterType<Debug>();
        Script script = new Script();
        string code = File.ReadAllText(Application.streamingAssetsPath + path + skill.Name + ".lua");
        SetGlobals(script, skill, User,Targets, vars);
        script.DoString(code);
        return script;
    }

    public static void SetGlobals(Script script, BattleSkill skill, Ship User,Ship[] Targets, Dictionary<string, object> vars = null)
    {
        script.Globals.Set("User", UserData.Create(User));
        script.Globals.Set("Skill", UserData.Create(skill));
        script.Globals.Set("Targets", UserData.Create(Targets));
        script.Globals.Set("Controller", UserData.Create(BattleController.Controller));
        script.Globals.Set("Prefabs", UserData.Create(BattlePrefabs.p));



        if (vars != null)
        {
            foreach (KeyValuePair<string, object> kvp in vars)
            {
                script.Globals.Set(kvp.Key, UserData.Create(kvp.Value));
            }
        }

        //script.Globals.Set()
    }

    public static YieldInstruction Wait(DynValue i)
    {
        string s = i.ToString().Replace("\"", "");

        if (s == "void") { return null; }
        string x = s.Split(' ')[0];

        if (x == "WaitForEndOfFrame")
        {
            return new WaitForEndOfFrame();
        }
        int n = Convert.ToInt32(s.Split(' ')[1]);
        if (x == "WaitForSeconds")
        {
            return new WaitForSeconds(n);
        }

        else {
            return null;
        }

    }

    

}
