using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class enemyAttackingHandler : MonoBehaviour
{


    public float health = 50f;

    public Slider healthBar;

    public int timeCounter = 0;
    public int timeCounter2 = 0;
    public bool canAttack = false;

    public float netDamage = 20f;
    public PhotonView PV;

    private bool canGetHit = true;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    void FixedUpdate(){
        if(timeCounter > 100)
        {
            canAttack = true;
            
        } else {
            canAttack = false;
        }
        timeCounter ++;


        if(canGetHit == false)
        {
            if(timeCounter2 > 25)
            {
                canGetHit = true;
                timeCounter2 = 0;
            }
            timeCounter2++;
        }
    }


    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerAttack>().isAttacking && canGetHit)
        {
            checkPlayerCollision(col);
        }
        
    }


    public void checkPlayerCollision(Collision2D col)
    {
        //used to determine which direction the attack came from
        float xDifference = this.transform.position.x - col.transform.position.x; //enemy is to the left of player if positive
        float yDifference = this.transform.position.y - col.transform.position.y; //enemy is below the player if positive
        //0 : attacking player is facing down
        //1 : attacking player is facing up
        //2 : attacking player is facing right
        //3 : attacking player is facing left
        int idleValue = col.gameObject.GetComponent<PlayerMovement>().idleValue; //the direction the player attacking me is facing
        float damageDealt = col.gameObject.GetComponent<DamageHandler>().netDamage; //the damage dealt to me

        Debug.Log("Attacked by Player and the idle value is " + idleValue + " and the netDamage is " + damageDealt);
                    
        //checks if the attack was from the horizontal direction or vertical direction
        if(Mathf.Abs(xDifference) > Mathf.Abs(yDifference)) { //attacked by player from left or right if true
                        
            if(xDifference > 0){ //attacked by player from the left if true
                            
                if(idleValue == 2){//hit from left
                    Debug.Log("Enemy hit by PLAYER from the LEFT");
                    PV.RPC("RPC_enemyAttacked", RpcTarget.AllBuffered, damageDealt);
                    canGetHit = false;
                }

            } else { //hit from right
                            
                if(idleValue == 3){
                    Debug.Log("Enemy hit by PLAYER from the RIGHT");
                    PV.RPC("RPC_enemyAttacked", RpcTarget.AllBuffered, damageDealt);
                    canGetHit = false;
                }

            }

        } else{ //attacked by player from up or down
                        
            if(yDifference < 0){ //attacked by player from above
                if(idleValue == 0){
                    Debug.Log("Enemy hit by PLAYER from ABOVE");
                    PV.RPC("RPC_enemyAttacked", RpcTarget.AllBuffered, damageDealt);
                    canGetHit = false;
                }
                            
            } else { //attacked by player from below
                            
            if(idleValue == 1){
                    Debug.Log("Enemy hit by PLAYER from BELOW");
                    PV.RPC("RPC_enemyAttacked", RpcTarget.AllBuffered, damageDealt);
                    canGetHit = false;
                }
            }

        }

    }


    //This is what happens when the enemy gets attacked by a player
    [PunRPC]
    void RPC_enemyAttacked(float damage){

        gameObject.GetComponent<enemyAttackingHandler>().health -= damage;

    }


}
