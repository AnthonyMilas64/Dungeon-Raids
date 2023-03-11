using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{

    public static GameSetup GS;

    public Transform[] spawnPoints;

    public Transform enemySpawnPoint;

    private void OnEnable()
    {
        if(GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    public void spawnRuby()
    {
        Debug.Log("SpawnRuby happened");
        PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "Ruby"),
            transform.position, transform.rotation, 0);
    }

    public void DisconnectPlayer()
    {
        Debug.Log("Disconnect Player ran");
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(MultiplayerSetting.multiplayerSetting.menuScene);
    }

}
