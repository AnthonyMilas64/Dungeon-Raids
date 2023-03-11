using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, items.Length);
        Instantiate(items[rand], transform.position, Quaternion.identity);
    }

    
}
