using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AvatarSetup : MonoBehaviour
{

    private PhotonView PV;
    public int characterValue;
    public GameObject myCharacter;
    

    
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        
        if(PV.IsMine)
        {
            Debug.Log("In the Start Method mySelectedCharacter is " + PlayerInfo.PI.mySelectedCharacter);
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {

        if(whichCharacter == 1)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Animation/PlayerRedAnimations/PlayerRed") as RuntimeAnimatorController;
            Debug.Log("The Color of the Avatar was switched because the whichCharacter value is " + whichCharacter);
        }
        
        characterValue = whichCharacter;
        //myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter], transform.position, transform.rotation, transform);
    }
}
