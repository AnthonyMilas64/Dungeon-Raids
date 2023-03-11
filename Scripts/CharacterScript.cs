using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterScript : MonoBehaviour
{
    
    public GameObject[] characters;
    public Image characterChosen;

    public void chooseCharacter(int whichCharacter){

        if(PlayerInfo.PI != null)
        {
            PlayerInfo.PI.mySelectedCharacter = whichCharacter;
            //PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }

        // StateNameController.chosenPlayer = whichCharacter;
        // StateNameController.chosenPlayerPrefab = characters[whichCharacter];
        characterChosen.sprite = characters[whichCharacter].GetComponent<SpriteRenderer>().sprite;

    }

}
