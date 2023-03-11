using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNameController : MonoBehaviour
{
    

    //public static int chosenPlayer = 0;
    //public static GameObject chosenPlayerPrefab;
    public static int perkModifier = 1;

    public static GameObject chosenWeaponPrefab = null;
    public static GameObject[] itemsChosen = new GameObject[5];

    public static int coins = 0;

    public static bool LoggedIn = false;
    
    public static bool isDead = false;

    public static string sceneChosen = "Game";



}
