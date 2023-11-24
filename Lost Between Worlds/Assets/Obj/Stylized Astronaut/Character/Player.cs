using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;

    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;

    //Added for jumping
    private Vector3 jumpHeight = Vector3.zero;

    void Start()
    {
        /*  controller = GetComponent<CharacterController>();
         anim = gameObject.GetComponentInChildren<Animator>(); */
    }

    void Update()
    {
        /*     if (Input.GetKey("w"))
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetInteger("AnimationPar", 0);
            } */

        /*   if (Input.GetKey("leftShift"))
          {
              speed = speed * 2;
          } */
        /* else
        {
            speed = 600.0f;
        } */

        //Added for jumping
        /*  if (Input.GetKey("space"))
         {
             jumpHeight = transform.up * speed * 0.08f * gravity;
             anim.SetInteger("AnimationPar", 1);
         }
         else
         {
             jumpHeight = Vector3.zero;
         }
         //Added for jumping

         if (controller.isGrounded)
         {
             moveDirection = transform.forward * Input.GetAxis("Vertical") * speed + jumpHeight;
         }

         float turn = Input.GetAxis("Horizontal");
         transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
         controller.Move(moveDirection * Time.deltaTime);
         moveDirection.y -= gravity * Time.deltaTime; */
    }
}
