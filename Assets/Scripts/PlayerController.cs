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
    // Start is called before the first frame update
    void Start()
    {
        //Rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;
        jumpPressedRemember -= Time.deltaTime;
     
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
        else
        {
            moveDirection.y += (Physics.gravity.y * graviyScale * Time.deltaTime);
        }

        controller.Move(moveDirection*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            Debug.Log("You Died");
        }
    }
}
