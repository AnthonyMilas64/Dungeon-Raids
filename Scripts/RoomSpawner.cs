using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    
    public int openingDirection;
    // 1 --> need Bottom Door
    // 2 --> need top Door
    // 3 --> need left Door
    // 4 --> need right Door

    // 5 --> need Bottom Open Room
    // 6 --> need top Open Room
    // 7 --> need left Open Room
    // 8 --> need right Open Room

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    void Start(){

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn(){
        
        if(!spawned){
            if(openingDirection == 1){
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);

            } else if(openingDirection == 2){
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            } else if(openingDirection == 3){
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            } else if(openingDirection == 4){
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            } else if(openingDirection == 5){
                // Need to spawn a room with an OPEN BOTTOM door.
                rand = Random.Range(0, templates.openBottomRooms.Length);
                Instantiate(templates.openBottomRooms[rand], transform.position, Quaternion.identity);

            } else if(openingDirection == 6){
                // Need to spawn a room with an OPEN TOP door.
                rand = Random.Range(0, templates.openTopRooms.Length);
                Instantiate(templates.openTopRooms[rand], transform.position, Quaternion.identity);
            } else if(openingDirection == 7){
                // Need to spawn a room with an OPEN LEFT door.
                rand = Random.Range(0, templates.openLeftRooms.Length);
                Instantiate(templates.openLeftRooms[rand], transform.position, Quaternion.identity);
            } else if(openingDirection == 8){
                // Need to spawn a room with an OPEN RIGHT door.
                rand = Random.Range(0, templates.openRightRooms.Length);
                Instantiate(templates.openRightRooms[rand], transform.position, Quaternion.identity);
            }

            //Debug.Log(templates);
            spawned = true;
        }   

        
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        
            if(other != null && other.CompareTag("SpawnPoint")){
                
                //Debug.Log(other.tag);
                if(!other.GetComponent<RoomSpawner>().spawned && !spawned){
                    //spawn walls blocking off any opening
                    if(other.transform.position.x != 0 || other.transform.position.y != 0){
                        Debug.Log("other position: " + other.transform.parent.parent.transform.position + "   Other OpeningDirection:" + findOtherDoor(other.gameObject) +
                         "  Other Child Count: " + other.transform.parent.transform.childCount +
                               "   My position: " + gameObject.transform.parent.parent.transform.position + "   My OpeningDirection:" + findOtherDoor(gameObject) +
                         "  My Child Count: " + gameObject.transform.parent.transform.childCount);
                        
                        Destroy(other.transform.parent.parent.gameObject);
                        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
                        Instantiate(templates.singleRooms[findOtherDoor(other.gameObject)-1], other.transform.parent.parent.transform.position, Quaternion.identity);

                        Instantiate(templates.singleRooms[findOtherDoor(gameObject)-1], gameObject.transform.parent.parent.transform.position, Quaternion.identity);
                        Destroy(gameObject.transform.parent.parent.gameObject);

                        
                    }
                    
                    
                }
                spawned = true;

            }

            
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
