using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other != null && other.CompareTag("SpawnPoint")){
            Destroy(other.gameObject);
        }

    }
}
