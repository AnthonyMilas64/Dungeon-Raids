using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour

{

    public float moveSpeed = 20f;
    void Update(){

        //Debug.Log(Input.GetAxisRaw("Horizontal") + " " + Input.GetAxisRaw("Vertical"));
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0));
    }
}
