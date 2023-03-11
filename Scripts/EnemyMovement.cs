using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;  

public class EnemyMovement : MonoBehaviour
{

    private Transform player = null;
    private bool isTracking = false;

    public float moveSpeed = 1f;

    public Vector2 velocity = new Vector2(1000.0f, 1000.0f);

    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Is Tracking is " + isTracking);

        if(isTracking)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            
            for (int i = 0; i < 360; i+=2)
            {
                gameObject.transform.eulerAngles = Vector3.forward * i;
                Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
                Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

                RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

                foreach (RaycastHit2D hit in hits)
                {

                    GameObject other = hit.collider.gameObject;
                    if(other.tag == "Player")
                    {
                        player = other.transform;
                        isTracking = true;
                        return;
                        
                    }
                    
                }
            }
            
        }
        
    }

    private void FixedUpdate()
    {
        //Debug.Log("The Enemy is running Fixed Update");
        if(isTracking)
        {
            moveCharacter(movement);
        }
        
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
