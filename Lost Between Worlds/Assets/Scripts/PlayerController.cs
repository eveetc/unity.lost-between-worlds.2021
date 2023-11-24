using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /*
       following code imported from asset
       */
    private Animator anim;
    private CharacterController controller;

    public float speed = 1.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

    /*
       import end
    */

    public GameObject runParticles;

    private Vector3 jumpHeight = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    public void showRunParticles()
    {
        print("triggered show particles");
        runParticles.SetActive(true);
    }

    void Update()
    {
        bool walkKey = Input.GetKey("w") || Input.GetKey("s");
        bool isWalking = anim.GetBool("isWalking");

        bool runKey = Input.GetKey("left shift");
        bool isRunning = anim.GetBool("isRunning");

        bool jumpKey = Input.GetKey("space");
        bool isJumping = anim.GetBool("isJumping");
        bool jumped = false;
        bool firstSpacePress = false;
        bool secondSpacePress = false;
        bool isFlipping = anim.GetBool("isFlipping");
        Vector3 jumpHeight = Vector3.zero;

        //walking functionality
        if (!isWalking && walkKey)
        {
            anim.SetBool("isWalking", true);
            speed = 5.0f;
        }
        else if (isWalking && !walkKey)
        {
            anim.SetBool("isWalking", false);
        }

        //running functionality
        if (!isRunning && (runKey && walkKey))
        {
            anim.SetBool("isRunning", true);
            speed = 10.0f;
        }
        else if (isRunning && (!runKey || !walkKey))
        {
            anim.SetBool("isRunning", false);
            speed = 5.0f;
        }

        if (!isRunning)
        {
            runParticles.SetActive(false);

        }

        //jumping functionality
        if (!isRunning && !isJumping && jumpKey)
        {
            anim.SetBool("isJumping", true);
            jumped = true;
            jumpHeight = transform.up * speed * 0.08f * gravity;
        }
        else if (isJumping && (!jumpKey || jumped))
        {
            jumped = false;
            jumpHeight = Vector3.zero;
            anim.SetBool("isJumping", false);
        }

        //flipping functionality
        if (isRunning && !isFlipping && jumpKey)
        {
            anim.SetBool("isFlipping", true);

            jumpHeight = transform.up * speed * 0.16f * gravity;
        }
        else if (isFlipping && !jumpKey)
        {
            jumpHeight = Vector3.zero;
            anim.SetBool("isFlipping", false);
        }

        if (controller.isGrounded)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed + jumpHeight;
        }


        /*
        following code imported from asset
        */
        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);


        controller.Move(Vector3.ClampMagnitude(moveDirection, 10.0f) * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;


        /*
        import end
        */
    }
}
