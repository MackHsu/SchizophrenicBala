using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    float horizontalMove = 0f;
    public float moveSpeed = 100;
    Transform playerTransform;
    Transform groundTransfrom;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        groundTransfrom = GameObject.Find("Ground").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
        if (playerTransform.position.x < groundTransfrom.position.x - groundTransfrom.lossyScale.x / 2)
        {
            playerTransform.position = new Vector3(groundTransfrom.position.x - groundTransfrom.lossyScale.x / 2, playerTransform.position.y, playerTransform.position.z);
        }
    }
}
