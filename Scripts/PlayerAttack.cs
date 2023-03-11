using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;


    private GameObject playerPrefab;

    public bool isAttacking = false;
    private bool isShootingBow = false;

    private bool canGetHit = true;

    public PhotonView PV;
    private int timeCounter = 0;

    public float arrowBaseSpeed = 7f;
    public GameObject arrowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //weaponPrefab = StateNameController.chosenWeaponPrefab;
        //playerPrefab = StateNameController.chosenPlayerPrefab;
        PV = GetComponent<PhotonView>();
    }


    void FixedUpdate(){
        //Debug.Log("CANGETHIT: " + canGetHit);
        if(canGetHit == false)
        {
            if(timeCounter > 25)
            {
                canGetHit = true;
                timeCounter = 0;
            }
            timeCounter++;
        }
        
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void Attack(){

        if(PV.IsMine)
        {
            if(!isShootingBow && !isAttacking)
                {
                    if(StateNameController.chosenWeaponPrefab != null)
                    {

                        //PV.RPC("RPC_setCanAttack", RpcTarget.AllBuffered, false);

                        if( StateNameController.chosenWeaponPrefab.GetComponent<weaponHandler>().type == "melee"){
                            throwPunch();
                        } else if( StateNameController.chosenWeaponPrefab.GetComponent<weaponHandler>().type == "bow"){
                            
                            drawArrow();
                        }


                    }
                }
        }
            


    }

    void shootArrow(){
        //Attack("shootArrow");

        if(PV.IsMine)
        {

            isShootingBow = false;
            gameObject.GetComponent<DamageHandler>().stamina -=  StateNameController.chosenWeaponPrefab.GetComponent<weaponHandler>().staminaRequired;
            animator.SetBool("isShootingBow", isShootingBow);

            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            

            int degreesRotated = 0;
            int playerIdleValue = GetComponent<PlayerMovement>().idleValue;
            if(playerIdleValue == 3){
                degreesRotated = 90;
                arrow.GetComponent<ArrowController>().velocity = new Vector2(-1 * arrowBaseSpeed, 0.0f);
            } else if(playerIdleValue == 2){
                degreesRotated = -90;
                arrow.GetComponent<ArrowController>().velocity = new Vector2(1 * arrowBaseSpeed, 0.0f);
            } else if(playerIdleValue == 1){
                degreesRotated = 0;
                arrow.GetComponent<ArrowController>().velocity = new Vector2(0.0f, 1 * arrowBaseSpeed);
            } else if(playerIdleValue == 0){
                degreesRotated = 180;
                arrow.GetComponent<ArrowController>().velocity = new Vector2(0.0f, -1 * arrowBaseSpeed);
            }

            arrow.GetComponent<ArrowController>().archer = gameObject;

            arrow.transform.eulerAngles = Vector3.forward * degreesRotated;

        }

        
    }

    public void drawArrow(){
        if(PV.IsMine)
        {
            isShootingBow = true;
            animator.SetBool("isShootingBow", isShootingBow);
        }
        
    }

    public void throwPunch(){
        //Attack("throwPunch");
        if(PV.IsMine)
        {
            PV.RPC("RPC_setIsAttacking", RpcTarget.AllBuffered, true);
            animator.SetBool("isAttacking", isAttacking);
        }
        
        //Debug.Log("throwPunch");
    }

    void endPunch(){
        if(PV.IsMine)
        {
            PV.RPC("RPC_setIsAttacking", RpcTarget.AllBuffered, false);
            gameObject.GetComponent<DamageHandler>().stamina -=  StateNameController.chosenWeaponPrefab.GetComponent<weaponHandler>().staminaRequired;
            animator.SetBool("isAttacking", isAttacking);
        }
        
        //Debug.Log("endPunch");

    }


    [PunRPC]
    void RPC_setIsAttacking(bool val)
    {
        isAttacking = val;
    }


    private void OnCollisionStay2D(Collision2D col)
    {
        //Debug.Log("Attack Collision Detected");
            if(PV.IsMine)
            {

                //Debug.Log("Attack Collision Detected #2");

                //Debug.Log("can get hit is " + canGetHit);
                if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerAttack>().isAttacking && canGetHit)
                {
                    checkPlayerCollision(col);
                }
                else if(col.gameObject.tag == "enemy" && col.gameObject.GetComponent<enemyAttackingHandler>().canAttack && canGetHit)
                {
                    checkEnemyCollisions(col);
                }
                

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
                    Debug.Log("hit by PLAYER from the LEFT");
                    PV.RPC("RPC_AttackedByEnemy", RpcTarget.AllBuffered, damageDealt);
                    canGetHit = false;
                }

            } else { //hit from right
                            
                if(idleValue == 3){
                    Debug.Log("hit by PLAYER from the RIGHT");
                    PV.RPC("RPC_AttackedByEnemy", RpcTarget.AllBuffered, damageDealt);
                    canGetHit = false;
                }

            }

        } else{ //attacked by player from up or down
                        
            if(yDifference < 0){ //attacked by player from above
                if(idleValue == 0){
                    Debug.Log("hit by PLAYER from ABOVE");
                    PV.RPC("RPC_AttackedByEnemy", RpcTarget.AllBuffered, damageDealt);
                    canGetHit = false;
                }
                            
            } else { //attacked by player from below
                            
            if(idleValue == 1){
                    Debug.Log("hit by PLAYER from BELOW");
                    PV.RPC("RPC_AttackedByEnemy", RpcTarget.AllBuffered, damageDealt);
                    canGetHit = false;
                }
            }

        }

    }


    public void checkEnemyCollisions(Collision2D col)
    {

        Debug.Log("Attacked by Enemy");
        float damageDealt = col.gameObject.GetComponent<enemyAttackingHandler>().netDamage;
        col.gameObject.GetComponent<enemyAttackingHandler>().timeCounter = 0;
                    
        PV.RPC("RPC_AttackedByEnemy", RpcTarget.AllBuffered, damageDealt);
        canGetHit = false;

    }


    //This is what happens when I get attacked by the other player
    [PunRPC]
    void RPC_AttackedByEnemy(float damage){

        gameObject.GetComponent<DamageHandler>().health -= damage;

    }



}
