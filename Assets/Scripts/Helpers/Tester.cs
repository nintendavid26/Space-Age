using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions.Coroutines;
using MoonSharp.Interpreter;
using System;

public class Tester : MonoBehaviour {

	
    void Start()
    {
        Debug.Log("Start");
        StartCoroutine(MoonSharpTest());
        //StartCoroutine(this.Sequence(CoroutineOne(), CoroutineTwo(), CoroutineThree(), CoroutineFour()));
        Debug.Log("End");
    }

    public void Click()
    {
        Debug.Log("click");
    }

    IEnumerator MoonSharpTest()
    {
        string code = @"
	return function()
		x = 1
		while x<4 do
			x = x + 1
			coroutine.yield('WaitForSeconds '..x)
		end
        Tester.debug('Done')
	end
	";

        // Load the code and get the returned function
        Script script = new Script();

        UserData.RegisterType<Tester>();
        UserData.RegisterType<WaitForSeconds>();
        //script.Globals.Set("Constant", UserData.Create(1));

        script.Globals.Set("Tester", UserData.Create(this));
        //int number = 1;
        //script.Globals.Set("number", UserData.Create(number));



        DynValue function = script.DoString(code);

        // Create the coroutine in C#
        DynValue coroutine = script.CreateCoroutine(function);

        DynValue x;
        x = coroutine.Coroutine.Resume();
        yield return Wait(x);
        // Resume the coroutine forever and ever..
        while (true)
        {

            if (x.ToString() != "void")
            {
                x = coroutine.Coroutine.Resume();
                yield return Wait(x);
                Debug.Log(x);
            }
            else { yield return null; }
            
        }

    }

    public void debug(string s)
    {
        Debug.Log("Test LUA: "+s);
    }


    public static YieldInstruction Wait(DynValue i)
    {
        string s = i.ToString().Replace("\"", "");


        Debug.Log(s);
        if (s == "void") { return null; }
        string x = s.Split(' ')[0];

        if (x == "WaitForEndOfFrame")
        {
            Debug.Log("Frame");
            return new WaitForEndOfFrame();
        }
        int n = Convert.ToInt32(s.Split(' ')[1]);
        if (x == "WaitForSeconds")
        {
            Debug.Log("Seconds");
            return new WaitForSeconds(n);
        }

        else {
            Debug.Log("Null");
            return null;
        }

    }


}
