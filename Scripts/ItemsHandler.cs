using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsHandler : MonoBehaviour
{
    
    public string type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(type == "coin")
            {
                coinAction();
            }
            else if(type == "chest")
            {
                chestAction();
            }
            else if(type == "ruby")
            {
                rubyAction();
            }
        }
        
    }

    private void coinAction(){
        Debug.Log("CoinedWasTriggered");
        StateNameController.coins += 5;
        Destroy(gameObject);
    }

    private void chestAction(){
        Debug.Log("ChestWasTriggered");
        StateNameController.coins += 15;
        Destroy(gameObject);
    }

    private void rubyAction(){
        Debug.Log("rubyWasTriggered");
        StateNameController.coins += 100;
        Destroy(gameObject);
    }


}
