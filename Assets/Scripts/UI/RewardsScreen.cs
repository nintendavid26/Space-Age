using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsScreen : MonoBehaviour {

    public List<EnemyShip.Rewards> Rewards;
    public Text text;

    public void Initialize(List<EnemyShip.Rewards> rewards)
    {
        gameObject.SetActive(true);
        Rewards = rewards;
        int Money=0, Exp=0;
        List<string> Items=new List<string>();

        foreach (EnemyShip.Rewards reward in Rewards)
        {
            Money += reward.MoneyReward;
            Exp += reward.ExpReward;
            if (reward.Item != "")
            {
                Items.Add(reward.Item);
            }

            text.text = "Money: " + Money + "\nExp: " + Exp+"\nItems:\n";
            foreach (string Item in Items)
            {
                text.text = text.text + Item + "\t";
            }
        }

    }
	
}
