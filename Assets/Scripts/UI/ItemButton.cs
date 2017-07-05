using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerEnterHandler
{

    public string s;
    public ItemsContainer Container;
    public ItemShop Shop;
    public Button button;
    public EventTrigger ET;
    public bool shop=false;


    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item =new Item(s);
        if (shop) {

            Shop.Description.text = item.Name + ":\n" + item.Description + "\nOwned x" + PlayerShip.GetItemAmt(s);

        }
        else {
            Container.Description.text = item.Name + ": " + item.Description;
        }
    }


}
