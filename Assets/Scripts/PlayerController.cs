using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float x, y;

    private float runSpeed = 7;
    private float rotationSpeed = 250;

    public Animator animator;

    public Rigidbody rb; 
    public float jumpHeight = 1.5f;

    public Transform groundCheck; 
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    bool isGrounded;
    bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
       // player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);

        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);
        /* 
         * 
         * NUEVAS ANIMACIONES CON OTRAS TECLAS
         * 
         * 
        if (Input.GetKey("TECLA"))
        {
            animator.SetBool("other", false);
            animator.Play("ACCION TECLA");
        }
        if (Input.GetKey("TECLA"))
        { 
            animator.SetBool("other", false);
            animator.Play("ACCION TECLA");
        }*/
        if(x>0 || x<0 || y>0 || y < 0)
        {
            animator.SetBool("other", true);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKey("space") && isGrounded && canJump)
        {
            animator.Play("Jump");
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            canJump = false; // Set the flag to false to prevent immediate subsequent jumps
            Invoke("ResetJumpCooldown", 1f); // Schedule resetting the flag after 1 second
        }
    }

    void ResetJumpCooldown()
    {
        canJump = true; // Reset the flag to allow jumping again
    }
    public void Jumpp()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

}
