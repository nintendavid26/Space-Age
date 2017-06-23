using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;
using UnityEngine.UI;

//TODO Choose skill window occasionaly doesn't show up after using it a second time
public class SkillContainer : MonoBehaviour
{
    public Button button;
    public Text Description;
    public List<Button> Buttons;
    public int selected=-1;
    public PlayerShip Ship;
    public Image grid;
    public void Initialize(PlayerShip ship)
    {
        Ship = ship;
        selected = -1;
        for (int i=0;i<ship.CurrentSkills.Count; i++)
        {
            int j = i;
            BattleSkill skill = ship.CurrentSkills[i];
            Button B = Instantiate(button, grid.transform);
            B.transform.GetChild(0).GetComponent<Text>().text = skill.Name;
            B.transform.GetChild(1).GetComponent<Text>().text = skill.Cost+"";
            B.onClick.AddListener(() => SetButton(j));
            Buttons.Add(B);
        }
        SetButton(0);
    }

    public void SetButton(int i)
    {
        if (selected == i&& Ship.CurrentSkills[i].CanUse(Ship))
        {
            BattleController.Controller.SelectedCommand = Ship.CurrentSkills[i];
            foreach(Button B in Buttons)
            {
                Destroy(B.gameObject);
            }
            Buttons = new List<Button>();
            gameObject.SetActive(false);
        }
        else
        {
            selected = i;
            BattleSkill skill = Ship.CurrentSkills[i];
            Description.text=skill.Name+"  Fuel:"+skill.Cost+" "+skill.Description;

        }
    }
}
