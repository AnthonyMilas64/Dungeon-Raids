using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    [Header("Open Rooms")]
    public GameObject[] openBottomRooms;
    public GameObject[] openTopRooms;
    public GameObject[] openLeftRooms;
    public GameObject[] openRightRooms;

    [Header("Single Rooms")]
    public GameObject[] singleRooms;


    void Start(){

        //Instantiate(bottomRooms[2], new Vector2(-20f, 0f), Quaternion.identity);

    }

}
