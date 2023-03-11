using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFbase : MonoBehaviour
{
   public void UpdateAllFB()
    {
        //Coins, Weapons, Perks, Flist
        DatabaseManager.getuserCoins();
        for (int i = 0; i<8; i++)
        {
            DatabaseManager.getWeaponcount(i);
        }
        for (int i = 0; i < 4; i++)
        {
            DatabaseManager.getPerkscount(i);
        }
    }


    public static void FBTEST()
    {
        Debug.Log(DatabaseManager.UserCoins);
        for (int i = 0; i <8; i++)
        {
            Debug.Log(DatabaseManager.UserWeapons[i]);
        }
        
    }


}

