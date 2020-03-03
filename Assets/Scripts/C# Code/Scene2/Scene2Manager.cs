using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.save.scene = 2;
        playerMovement.moveSwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
