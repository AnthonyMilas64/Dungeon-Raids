using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    public float rollSpeed = 8f;

    public Rigidbody2D rb;
    public Animator animator;

    public Joystick joystick;

    public int idleValue;

    private bool isMine = true;

    Vector2 movement;
    Vector2 movementRolling;
    bool isPlayerMoving;
    bool isPlayerRolling;

    public GameObject PlayerCameraGame;
    public GameObject PlayerCameraRaid;
    public PhotonView PV;
    public SpriteRenderer sr;
    public TMP_Text PlayerNameText;
    public GameObject joystickCanvas;


    void Start()
    {
        //PV = GetComponent<PhotonView>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            CheckInput();
            Swipe();
        }
    }

    void FixedUpdate()
    {
       if(PV.IsMine)
        {
            FixedCheckInput();
        }
    }

    private void Awake()
    {
        if(PV.IsMine)
        {
            SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;

            if(StateNameController.sceneChosen == "Game") {
                PlayerCameraGame.SetActive(true);
            } else {
                PlayerCameraRaid.SetActive(true);
            }

            joystickCanvas.SetActive(true);
            PlayerNameText.text = AuthManager.username;
        }
        else
        {
            PlayerNameText.text = AuthManager.username;
            PlayerNameText.color = Color.cyan;
        }
    }

    private void CheckInput()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical"); 

        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
        }
       


        if(!isPlayerRolling)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        animator.SetFloat("IdleValue", idleValue);
    }

    private void FixedCheckInput()
    {
         if(isPlayerRolling) {
            rb.MovePosition(rb.position + movementRolling * rollSpeed * Time.fixedDeltaTime);
        } else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        if(PV.IsMine)
        {
            var touch = Input.touches[Input.touchCount - 1];
            if (touch.position.x > Screen.width/3)
            {
                Debug.Log("Right Side Swiped");
                //Debug.Log("Swipe in Direction: " + data.Direction.ToString());
                setRollingAnimation(idleValue);
            }
            else if (touch.position.x < Screen.width/3)
            {
                Debug.Log("Left Side Swiped");
            }
        }
        
    }

    private void setRollingAnimation(int direction) {
        if(canRoll())
        {
            switch(direction) 
            {
            case 1:
                movementRolling.x = 0;
                movementRolling.y = 1;
                break;
            case 0:
                movementRolling.x = 0;
                movementRolling.y = -1;
                break;
            case 3:
                movementRolling.x = -1;
                movementRolling.y = 0;
                break;
            case 2:
                movementRolling.x = 1;
                movementRolling.y = 0;
                break;
            }

            animator.SetFloat("Horizontal", movementRolling.x);
            animator.SetFloat("Vertical", movementRolling.y);

            isPlayerRolling = true;
            animator.SetBool("isRolling", isPlayerRolling);

            gameObject.GetComponent<DamageHandler>().stamina -= 12.5f;

        }
        
    }


    private bool canRoll(){
        return gameObject.GetComponent<DamageHandler>().stamina >= 25;
    }


    void playerMoving() {

        //isPlayerMoving = true;

        setIdleValue(movement);

    }

    void setIdleValue(Vector2 movement)
    {
        if( Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            if(movement.x > 0)
            {
                PV.RPC("RPC_changeIdleValue", RpcTarget.AllBuffered, 2);
            } else
            {
                PV.RPC("RPC_changeIdleValue", RpcTarget.AllBuffered, 3);
            }
        } else 
        {
            if(movement.y > 0)
            {
                PV.RPC("RPC_changeIdleValue", RpcTarget.AllBuffered, 1);
            } else
            {
                PV.RPC("RPC_changeIdleValue", RpcTarget.AllBuffered, 0);
            }
        }
    }


    [PunRPC]
    void RPC_changeIdleValue(int val)
    {
        idleValue = val;
    }


    void playerIdle() {
        //isPlayerMoving = false;
    }

    void startPlayerRoll()
    {

    }

    void endPlayerRoll() {
        isPlayerRolling = false;
        animator.SetBool("isRolling", isPlayerRolling);

        setIdleValue(movementRolling);

    }


    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public float swipeDistance;
    
    private void Swipe()
    {
        if(Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width/3)
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        }
        if(Input.GetMouseButtonUp(0) && Input.mousePosition.x > Screen.width/3)
        {
                //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        
                //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            
            //normalize the 2d vector
            currentSwipe.Normalize();
    
            //swipe upwards
            if(currentSwipe.y > 0 && currentSwipe.x > -swipeDistance && currentSwipe.x < swipeDistance)
            {
                //Debug.Log("up swipe");
                setRollingAnimation(idleValue);
            }
            //swipe down
            if(currentSwipe.y < 0 && currentSwipe.x > -swipeDistance && currentSwipe.x < swipeDistance)
            {
                //Debug.Log("down swipe");
                setRollingAnimation(idleValue);
            }
            //swipe left
            if(currentSwipe.x < 0 && currentSwipe.y > -swipeDistance && currentSwipe.y < swipeDistance)
            {
                //Debug.Log("left swipe");
                setRollingAnimation(idleValue);
            }
            //swipe right
            if(currentSwipe.x > 0 && currentSwipe.y > -swipeDistance && currentSwipe.y < swipeDistance)
            {
                //Debug.Log("right swipe");
                setRollingAnimation(idleValue);
            }

        }
    }

    // private void OnCollisionStay2D(Collision2D col)
    // {
    //     Debug.Log("Collision Detected");
    //     // if (isAttacking && other.gameObject.tag == "enemy")
    //     // {
    //     //     Debug.Log("Attacking enemy");
    //     // }
        
    // }

}
