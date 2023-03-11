using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{

    /*
     ID:
        Coins:
        Weapons:
            W1:
            W2:
            W3:
        Perks:
                Amnt of P1:
                Amnt of P2:
                Amnt of P3:
        Friends:
            Running List with ID's seperated by ,'s
     
     
     */

    public string IDentification;
    public string[] Weapons = { "0", "0", "0", "0", "0", "0", "0","0"};
    public String[] Perks = { "0", "0", "0", "0" };
    public string Coins = "10";
    public string Flist = " "; //friendslist
    public string currentgame = " "; // current game code

    public UserData(string UserID)
    {
        IDentification = UserID;
        string Coins = this.Coins;
        String[] Weapons = this.Weapons;
        String[] Perks = this.Perks;
        String Flist = this.Flist;
        String currentgame = this.currentgame; 
    }


}
