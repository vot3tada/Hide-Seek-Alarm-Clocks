using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float xMove;
    private float zMove;
    private Vector3 moveDirection;
    private float speed;
    private float gravity;

    private Vector3 velocity;
    private CharacterController player;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        speed = 5f;
        gravity = -4f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");

        if (!player.isGrounded)
        {
            gravity -= 0.3f;
        }
        else
        {
            gravity = -4f;
            velocity.y = 0;
        }

        if (Input.GetAxis("Run") == 1)
            speed = 11f;
        else
            speed = 5f;

        if (Input.GetAxis("SitDown") == 1)
            player.height = 1;
        else player.height = 2;

        moveDirection = transform.right * xMove + transform.forward * zMove;
        player.Move(moveDirection * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);

    }


}
