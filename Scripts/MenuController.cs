using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Photon.Pun;
using Photon.Realtime;
//using Firebase.Unity.Editor;

public class MenuController : MonoBehaviourPunCallbacks
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private TMP_InputField CreateGameInput;
    //[SerializeField] private TMP_InputField JoinGameInput;
    private FirebaseDatabase _database;

    [SerializeField] private GameObject StartButton;

    public GameObject AuthCanvas;

    public GameObject[] panelsList;
    public TextMeshProUGUI CoinText;

    public static MenuController lobby;


    private void Awake()
    {
        
        lobby = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }

    
    void Update()
    {
        CoinText.text = "COINS: " + StateNameController.coins;

        AuthCanvas.SetActive(!StateNameController.LoggedIn);
    }

    public override void OnConnectedToMaster()
    {
        
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Player has Connected to the Photon Master Server");
    }

    

    
    private bool isOpen;
    private string sceneType;
    public void JoinGame(string type)
    {
        sceneType = type;
        StateNameController.sceneChosen = sceneType;

        if(sceneType == "Game"){
            isOpen = true;
            MultiplayerSetting.multiplayerSetting.multiplayerScene = 1;
            PhotonNetwork.JoinRandomRoom();
        } 
        else 
        {   
            isOpen = false;
            MultiplayerSetting.multiplayerSetting.multiplayerScene = 2;
            CreateRoom();
        }

        
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random room but it failed");
        CreateRoom();
    }

    void CreateRoom()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = isOpen, MaxPlayers = (byte) MultiplayerSetting.multiplayerSetting.maxPlayers};
        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("failed to create a new room, there's another room with same name");
        CreateRoom();
        
    }

    public override void OnJoinedRoom()
    {

        Debug.Log("OnJoinedRoom");
        //PhotonNetwork.LoadLevel(sceneType);
        // joined a room successfully
    }


    public void backToAuthCanvas()
    {
        StateNameController.LoggedIn = false;
    }


    public void doExitGame() {
        Application.Quit();
    }


    public void disableAll(GameObject enabledGM){
        foreach (GameObject panel in panelsList)
        {
            if(panel == enabledGM)
            {
                panel.SetActive(true);
            } 
            else
            {
                panel.SetActive(false);
            }
        }
    }


}
