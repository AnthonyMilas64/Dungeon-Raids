using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public GameObject archer;

    void Update() {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        Debug.DrawLine(currentPosition, newPosition, Color.red);

        foreach (RaycastHit2D hit in hits)
        {

            GameObject other = hit.collider.gameObject;
            if(other != archer && other.tag != "SpawnPoint" && other.tag != "Center") {

                if(other.tag == "enemy" || other.tag == "Player"){
                    Debug.Log("ENEMY HIT");
                    other.GetComponent<enemyAttackingHandler>().health -= StateNameController.chosenWeaponPrefab.GetComponent<weaponHandler>().damage;
                }

                Destroy(gameObject);
                
            }
            
        }

        transform.position = newPosition;

    }

}
