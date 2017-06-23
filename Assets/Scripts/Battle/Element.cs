using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum element {None,Fire,Electric,Ice,Lazer,Nano,Dark,Light}

public class Element {

    public Element[] Strengths,Weaknesses,Immunes;
    public string Name;
    public string Description;

    public Element(string name,string description, Element[] strengths,Element[] weaknesses,Element[] immunes)
    {
        Name = name;
        Description = description;
        Strengths = strengths;
        Weaknesses = weaknesses;
        Immunes = immunes;

    }
    public Element(string name, Element[] strengths, Element[] weaknesses, Element[] immunes)
    {
        Name = name;
        Strengths = strengths;
        Weaknesses = weaknesses;
        Immunes = immunes;

    }


    public readonly static Element Fire = new Element("Fire",
                                 new Element[] {Ice,Nano},
                                 new Element[] {Dark},
                                 new Element[] { });

    public readonly static Element Electric = new Element("Electric",
                                 new Element[] { Lazer,Nano },
                                 new Element[] { Dark,Ice },
                                 new Element[] { });

    public readonly static Element Ice = new Element("Ice",
                                new Element[] { Dark,Nano },
                                new Element[] { Fire,Light,Lazer,Electric},
                                new Element[] { });

    public readonly static Element Lazer = new Element("Lazer",
                             new Element[] { Fire,Nano },
                             new Element[] { Dark },
                             new Element[] { });

    public readonly static Element Nano = new Element("Nano",
                                new Element[] { Light, Dark },
                                new Element[] { Fire,Electric,Lazer },
                                new Element[] { });

    public readonly static Element Dark = new Element("Dark",
                               new Element[] { Fire,Electric,Ice,Lazer},
                               new Element[] { Nano, Light },
                               new Element[] { });

    public readonly static Element Light = new Element("Light",
                           new Element[] { Fire, Electric, Ice, Lazer},
                           new Element[] { Nano, Dark },
                           new Element[] { });

    public readonly static Element None = new Element("None",
                                new Element[] { },
                                new Element[] { },
                                new Element[] { });
    public float DmgMultiplier(Element Defender,Element Attacker)
    {
        float i = 1;
        if (Defender.Strengths.Contains(Attacker))
        {
            i = 1/2;
        }
        else if (Defender.Weaknesses.Contains(Attacker))
        {
            i = 2;
        }
        else if (Defender.Immunes.Contains(Attacker))
        {
            i = 0;
        }
        return i;

    }
    /*
    public static Dictionary<string, Element> Elements = new Dictionary<string, Element> {
        { },
        { },
        { } };
    */
    public static Element FromEnum(element e)
    {
        
        switch (e)
        {
            case element.Fire:
                return Fire;
            case element.Electric:
                return Electric;
            case element.Ice:
                return Ice;
            case element.Lazer:
                return Lazer;
            case element.Nano:
                return Nano;
            case element.Dark:
                return Dark;
            case element.Light:
                return Light;
            case element.None:
                return None;
            default:
                return None;
        }
    }
}
