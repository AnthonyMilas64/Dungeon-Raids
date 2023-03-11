using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    
    private PhotonView PV;
    public GameObject myAvatar;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        if(PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar1"),
                GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);
            
            if(MultiplayerSetting.multiplayerSetting.multiplayerScene == 2)
            {
                PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", "EnemyAvatar1"),
                    GameSetup.GS.enemySpawnPoint.position, GameSetup.GS.enemySpawnPoint.rotation, 0);
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
