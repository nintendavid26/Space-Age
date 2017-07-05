using Battle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : ShopMenu
{
    private List<Item> AvailabeItems;
    public List<Item> Items;
    public List<string> RevealedItems;
    public Button button;
    public List<ItemButton> Buttons;
    public Text Description;
    public override void Close()
    {
        foreach(ItemButton B in Buttons)
        {
            Destroy(B.gameObject);
        }
        Buttons=new List<ItemButton>();
        Description.text = "";
    }

    public override void initialize()
    {
        if (Items.Count == 0)
        {
            LoadItems();
        }
        AvailabeItems = Items.Where(x => x.Cost <= PlayerShip.Money).ToList();
        AvailabeItems = AvailabeItems.OrderBy(x => x.Cost).ToList();
        LoadButtons();
    }

    public override void Open()
    {
        throw new NotImplementedException();
    }

    public void LoadItems()
    {
        List<string> items = new List<string>();
        DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath + "/Items/JSON/");
        FileInfo[] f = d.GetFiles();
        foreach (FileInfo file in f)
        {
            if (file.Name.Split('.').Last() == "json")
            {
                items.Add(file.Name.Split('.')[0]);
            }
        }
        foreach(string item in items)
        {
            Items.Add(new Item(item));
        }
    }

    public void LoadButtons()
    {
        
        foreach (Item i in AvailabeItems)
        {   
            string j = i.Name;
            ItemButton B = Instantiate(button, transform).GetComponent<ItemButton>();
            B.s = j;
            B.transform.GetChild(0).GetComponent<Text>().text = j;
            B.transform.GetChild(1).GetComponent<Text>().text = i.Cost + "$";
            B.shop = true;
            B.Shop = this;
            B.button.onClick.AddListener(() => OnClick(j));
            Buttons.Add(B);
        }
    }

    public void OnClick(string s)
    {
        Item item = new Item(s);
        if (PlayerShip.Money < item.Cost) { return; }
        PlayerShip.GetItem(s);
        PlayerShip.Money -= item.Cost;
        Description.text = item.Name + ":\n" + item.Description + "\nOwned x" + PlayerShip.GetItemAmt(s);
        //Buttons = new List<ItemButton>();
        //gameObject.SetActive(false);
    }

}
