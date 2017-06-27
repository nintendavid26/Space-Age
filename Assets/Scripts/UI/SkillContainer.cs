using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//TODO Choose skill window occasionaly doesn't show up after using it a second time
public class SkillContainer : MonoBehaviour
{
    public Button button;
    public Text Description;
    public List<SkillButton> Buttons;
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
            SkillButton B = Instantiate(button, grid.transform).GetComponent<SkillButton>();
            B.Container = this;
            B.i = j;
            B.transform.GetChild(0).GetComponent<Text>().text = skill.Name;
            B.transform.GetChild(1).GetComponent<Text>().text = skill.Cost+"";
            B.button.onClick.AddListener(() => OnClick(j));
            Buttons.Add(B);
        }
    }


    public void OnClick(int i)
    {
        BattleController.Controller.SelectedCommand = Ship.CurrentSkills[i];
        foreach (SkillButton B in Buttons)
        {
            Destroy(B.gameObject);
        }
        Buttons = new List<SkillButton>();
        gameObject.SetActive(false);
    }

    

    
}
