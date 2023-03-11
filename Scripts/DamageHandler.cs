using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Photon.Pun;
using Photon.Realtime;

public class DamageHandler : MonoBehaviour
{

    public float health = 100f;
    public float stamina = 100f;

    public Slider healthBar;
    public Slider staminaBar;

    public PhotonView PV;

    private int timeCounter = 0;

    public float netDamage; //the most damage this player can give
    public float tempNetDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(StateNameController.chosenWeaponPrefab != null)
        {
            netDamage = StateNameController.chosenWeaponPrefab.GetComponent<weaponHandler>().damage * StateNameController.perkModifier;
            if(tempNetDamage != netDamage) //there was a change in net damage
            {
                PV.RPC("RPC_changeNetDamage", RpcTarget.AllBuffered, netDamage);
            }
            
        }
        
        if(health <= 0){
            
            
            GameSetup.GS.spawnRuby();
            GameSetup.GS.DisconnectPlayer();
            
        }

        healthBar.value = health;
        staminaBar.value = stamina;

        
    }




    void FixedUpdate(){

        if(timeCounter % 10 == 0)
        {
            if(stamina <= 99)
            {
                stamina += 1;
            }
            
        }


        if(timeCounter % 60 == 0)
        {
            if(health <= 99)
            {
                health += 1;
            }
            
        }




        timeCounter++;
    }


    [PunRPC]
    void RPC_changeNetDamage(float val)
    {
        netDamage = val;
        tempNetDamage = val;
    }





}
