using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
   

    // // Update is called once per frame
    // void Update()
    // {
        
    // }


    public Image[] inventoryImages;
    public Image selectedSlotImage;
    public TextMeshProUGUI CoinText;

    private int slotSelected = 0;



     // // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            if(StateNameController.itemsChosen[i] != null){
                inventoryImages[i].sprite = StateNameController.itemsChosen[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }


    void Update()
    {
        CoinText.text = "COINS: " + StateNameController.coins;
    }

    public void inventoryPressed(int slotNumber)
    {
        
        selectedSlotImage.sprite = inventoryImages[slotNumber].sprite;
        slotSelected = slotNumber;
        if(slotNumber < StateNameController.itemsChosen.Length){
            StateNameController.chosenWeaponPrefab = StateNameController.itemsChosen[slotNumber];
        }
        

    }


    public void viewItemsPressed()
    {
        Debug.Log("View Items");
    }


}
