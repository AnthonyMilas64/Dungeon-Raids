using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using Firebase;
using Firebase.Database;
using Photon.Pun;
using Photon.Realtime;
using Proyecto26;

public class FriendsController : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    
    // }

    // // Update is called once per frame
    // void Update()
    // {
    
    // }


    public TMP_InputField friendsInput;

    //public GameObject userNameTag;

    private int timesPressed = 0;

    //public ScrollView friendsList;
    public TextMeshProUGUI[] textList;

    public void addFriendClicked()
    {

        textList[timesPressed].text = friendsInput.text;
        timesPressed++;

    }


    public bool realaccount(string namefriend)
    {
        bool doner = false;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////safdsff
        RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + namefriend + ".json").Then(response => {
            Debug.Log(response.Weapons[0] + "INSIDE");
            Debug.Log("INSIDEsfsaf");
            doner = true;

        });
        //Debug.Log("OUSIDE");
        Debug.Log(doner);
        return doner;
    }

    public void TEST()
    {
        Debug.Log(DatabaseManager.UserCoins);
        DatabaseManager.updateuserCoins(1);
    }




    // Debug.Log("add friend was clicked");
        // Debug.Log(friendsInput.text);
        // //first look if you have this friend added, then is he actually in the database, then add the friend to your list. 

        // string Friendname = friendsInput.text;
        // RestClient.Get<UserData>("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json").Then(response => {
        //     //Debug.Log(response.Weapons[0] + "INSIDE");
        //     response.Weapons[0] = "afafds";
        //     string friendlist = response.Flist;
        //     if (friendlist.Contains(Friendname))
        //     {
        //         Debug.Log("alr Friends");
        //     }
        //     else
        //     {
        //         if (realaccount(Friendname)) // make sure its an account
        //         { 
        //             friendlist = friendlist + "," + Friendname;
        //             response.Flist = friendlist;
        //             RestClient.Delete("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json");
        //             RestClient.Put("https://battle-raids-database-default-rtdb.firebaseio.com/" + AuthManager.username + ".json", response);
        //             Debug.Log("ADDING"); 
        //         }
        //         else
        //         {
        //             Debug.Log("Account not found");
        //         }
        //     }
        // });
        // //RestClient.Put("https://battle-raids-database-default-rtdb.firebaseio.com/" + User.DisplayName + ".json", response);

    //Also make a thing where when the friends are loaded, the list view is made and every time something is added, it is cleared and than everything is added again so its called on start and when someting is created.
}