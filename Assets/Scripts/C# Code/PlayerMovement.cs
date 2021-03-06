﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float moveSpeed = 50;
    Transform playerTransform;
    Transform groundTransfrom;
    bool jump = false;
    bool canMove = true;
    public bool moveSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        groundTransfrom = GameObject.Find("Ground").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveSwitch || GameManager.focusStack.Count != 0) canMove = false;
        else canMove = true;

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }
        jump = false;
    }
}
