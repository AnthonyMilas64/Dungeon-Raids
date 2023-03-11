using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutsideEvents : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void quitPressed()
    {
        Debug.Log("Quit Pressed");
        GameSetup.GS.DisconnectPlayer();
    }


}
