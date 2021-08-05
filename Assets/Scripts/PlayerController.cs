using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    public CharacterController controller;
    public float jumpForce;
    public float graviyScale;
    public float jumpPressedRemember;

    public float jumpPressedRememberTime = 0.5f;

    private Vector3 moveDirection;
    public bool onLadder;

    // Start is called before the first frame update
    void Start()
    {
        //Rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveCallculation();
    }

    void MoveCallculation()
    {
        //move variables
        float yStore = moveDirection.y;
        if (!onLadder)
        {
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;
        }

        //jump variables
        jumpPressedRemember -= Time.deltaTime;

        #region jumping
        //Instead of checking if player is on the ground instantly it makes space for mistake (bunny hops are possible thanks to that, cuz players sucks)
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressedRemember = jumpPressedRememberTime;
        }

        if (controller.isGrounded)
        {
            moveDirection.y = -1;

            if (jumpPressedRemember > 0)
            {
                jumpPressedRemember = 0;
                moveDirection.y = jumpForce;
            }

        }
        //if player is not grounded make him fall
        else
        {
            moveDirection.y += (Physics.gravity.y * graviyScale * Time.deltaTime);
        }
        #endregion

        if (onLadder)
        {
            moveDirection = transform.up * Input.GetAxisRaw("Vertical");
            moveDirection = moveDirection.normalized * moveSpeed;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            Debug.Log("You Died");
        }

        if (collision.CompareTag("Ladder"))
        {
            onLadder = true;
            moveDirection = Vector3.zero;
            Debug.Log(onLadder);
            Debug.Log("You are on ladder");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LadderExit"))
        {
            onLadder = false;
            transform.position = other.transform.position;
        }
    }

}
