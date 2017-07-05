using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour,IPointerEnterHandler {

    public int i;
    public SkillContainer Container;
    public Button button;
    public EventTrigger ET;


    public void OnPointerEnter(PointerEventData eventData)
    {
        BattleSkill skill = Container.Ship.AvailableSkills[i];
        Container.Description.text = skill.Name + "  Fuel:" + skill.Cost + " " + skill.Description;
    }



}
