using Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemsContainer : MonoBehaviour {



    public Button button;
    public Text Description;
    public List<ItemButton> Buttons;
    public List<string> ItemNames;
    public int selected = -1;
    public PlayerShip Ship;
    public Image grid;
    public void Initialize(PlayerShip ship)
    {
        Ship = ship;
        selected = -1;
        ItemNames = PlayerShip.Items.Keys.ToList();
        for (int i = 0; i < ItemNames.Count; i++)
        {
            string j = ItemNames[i];
            ItemButton B = Instantiate(button, grid.transform).GetComponent<ItemButton>();
            B.Container = this;
            B.s = j;
            B.transform.GetChild(0).GetComponent<Text>().text = j;
            B.transform.GetChild(1).GetComponent<Text>().text = PlayerShip.Items[j]+"";
            B.button.onClick.AddListener(() => OnClick(j));
            Buttons.Add(B);

          
        }
    }


    public void OnClick(string s)
    {
        BattleController.Controller.SelectedCommand = new UseItem(new Item(s));
        foreach (ItemButton B in Buttons)
        {
            Destroy(B.gameObject);
        }
        Buttons = new List<ItemButton>();
        gameObject.SetActive(false);
    }


}
