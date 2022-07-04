using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    private float xMove;
    private float zMove;
    private Vector3 moveDirection;
    private float speed = 6F;
    private float speedMove = 6f;
    private float speedShift;
    private float gravity;
    private AudioSource[] footAudio;
    private int footAudioNumber = 0;
    private Vector3 velocity;
    private CharacterController player;
    private CreateHouseScript createHouseScript;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        speedShift = speed * 2;
        gravity = -4f;
        createHouseScript = GameObject.Find("CreateHouseHandler").GetComponent<CreateHouseScript>();
        footAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OutOfBounds();
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
        {
            speed = speedShift;
            footAudio[((int)Time.time) % footAudio.Length].pitch = 1.4f;
            if (footAudio.Count(audio => audio.isPlaying) == 0 && (xMove > 0 || zMove > 0))
            {
                footAudio[((int)Time.time) % footAudio.Length].Play();
            }
        }
        else
        {
            speed = speedMove;
            footAudio[((int)Time.time) % footAudio.Length].pitch = 1.1f;
            if (footAudio.Count(audio => audio.isPlaying) == 0 && (xMove > 0 || zMove > 0))
            {
                footAudio[((int)Time.time) % footAudio.Length].Play();
            }
        }
            

        if (Input.GetAxis("SitDown") == 1)
            player.height = 1;
        else player.height = 2;

        moveDirection = transform.right * xMove + transform.forward * zMove;
        player.Move(moveDirection * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);
    }
    private void OutOfBounds()
    {
        if (transform.position.y < -3)
        {
            player.enabled = false;
            transform.SetPositionAndRotation(createHouseScript.PlayerPosition, transform.rotation);
            //Debug.Log("Out");
            player.enabled = true;
        }
    }


}
