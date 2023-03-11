using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{

    public static String UserCoins;
    public static String[] UserWeapons = { "0", "0", "0", "0", "0", "0", "0", "0" };
    public static String[] UserPerks = { "0", "0", "0", "0" };
    public static string UserFlist = " "; //friendslist

    public static void getuserCoins() //gets coins, but due to delay doesnt update after call, must use later in another key press
    {
        String send = "0";
        Debug.Log(AuthManager.username);
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
            send = response.Coins;
            //Debug.Log("COINSSSSSSS " + send);
            UserCoins = send; 
        });
    }

    public static void updateuserCoins(int add)
    {
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
            int currentCoins = Convert.ToInt32(response.Coins);
            currentCoins += add;
            response.Coins = currentCoins.ToString();
            UserCoins = response.Coins;
            RestClient.Delete("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json");
            RestClient.Put("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json", response);
        });
    }

    public static void getWeaponcount(int loc)
    {
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
            String send = response.Weapons[loc];
            UserWeapons[loc] = send;
        });
    }

    public static void increaseWeaponcount(int loc)
    {
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
            int currentcount  = Convert.ToInt32(response.Weapons[loc]);
            currentcount++;
            response.Weapons[loc] = currentcount.ToString();
            RestClient.Delete("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json");
            RestClient.Put("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json", response);
        });
    }

    public static void DecreaseWeaponcount(int loc)
    {
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
            int currentcount = Convert.ToInt32(response.Weapons[loc]);
            currentcount--;
            response.Weapons[loc] = currentcount.ToString();
            RestClient.Delete("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json");
            RestClient.Put("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json", response);
        });
    }


    public static void getPerkscount(int loc)
    {
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
            String send = response.Perks[loc];
            UserPerks[loc] = send;
        });
    }

    public static void increasePerkscount(int loc)
    {
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
            int currentcount = Convert.ToInt32(response.Perks[loc]);
            currentcount++;
            response.Perks[loc] = currentcount.ToString();
            RestClient.Delete("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json");
            RestClient.Put("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json", response);
        });
    }

    public static void DecreasePerkscount(int loc)
    {
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
            int currentcount = Convert.ToInt32(response.Perks[loc]);
            currentcount--;
            response.Perks[loc] = currentcount.ToString();
            RestClient.Delete("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json");
            RestClient.Put("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json", response);
        });
    }

}
