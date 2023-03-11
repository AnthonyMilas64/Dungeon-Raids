using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{

    private int[] doorValues = { 2, 1, 4, 3, 6, 5, 8, 7};
    private RoomTemplates templates;

    // 1 --> need Bottom Door
    // 2 --> need top Door
    // 3 --> need left Door
    // 4 --> need right Door

    // 5 --> need Bottom Open Room
    // 6 --> need top Open Room
    // 7 --> need left Open Room
    // 8 --> need right Open Room
    
    void OnTriggerEnter2D(Collider2D other){
        
        if(other != null && other.CompareTag("SpawnPoint")){

            int otherDoor = other.GetComponent<RoomSpawner>().openingDirection;
            if(!containsDoor(doorValues[otherDoor - 1])) {
                Debug.Log("couldn't spawn room    other position: " + other.transform.parent.parent.transform.position +  
                    "   My position: " + gameObject.transform.position);

                Destroy(other.transform.parent.parent.gameObject);
                templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
                Instantiate(templates.singleRooms[findOtherDoor(other.gameObject)-1], other.transform.parent.parent.transform.position, Quaternion.identity);

            }
              
        }

    }


    //searches to see if a door is in a room
    private bool containsDoor(int door){
            for(int i = 0; i < gameObject.transform.parent.transform.childCount; i++){
                if(gameObject.transform.parent.transform.GetChild(i).gameObject.CompareTag("SpawnPoint")){
                     int tempDoor = gameObject.transform.parent.transform.GetChild(i).gameObject.GetComponent<RoomSpawner>().openingDirection;

                    if(tempDoor == door){
                        return true;
                    }
                }
               
            }
            return false;
        }


    private int findOtherDoor(GameObject spawnPoint){
        int currentSP = spawnPoint.GetComponent<RoomSpawner>().openingDirection;
        for(int i = 0; i < spawnPoint.transform.parent.transform.childCount; i++){
            int newSP = spawnPoint.transform.parent.transform.GetChild(i).gameObject.GetComponent<RoomSpawner>().openingDirection;

            if(newSP != currentSP){
                return newSP;
            }
        }
        return -1;

    }

}
