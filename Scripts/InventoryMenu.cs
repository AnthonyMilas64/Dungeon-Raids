using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour
{

    private int slotChosen = 0;
    public GameObject[] Items;

    public Image[] slotImages = new Image[5];
    
    
    void Start()
    {
        StateNameController.itemsChosen = new GameObject[5];
    }
    
    public void slotPressed(int slotNumber)
    {
        slotChosen = slotNumber;
    }


    
    public void itemPressed(int itemNumber){

        if(StateNameController.coins >= Items[itemNumber].GetComponent<weaponHandler>().price)
        {
            slotImages[slotChosen].sprite = Items[itemNumber].GetComponent<SpriteRenderer>().sprite;
            StateNameController.itemsChosen[slotChosen] = Items[itemNumber];
            StateNameController.coins -= Items[itemNumber].GetComponent<weaponHandler>().price;
        }

        
    }

}
