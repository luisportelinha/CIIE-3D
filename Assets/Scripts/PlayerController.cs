using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float x, y;

    private float runSpeed = 7;
    private float rotationSpeed = 250;

    public Animator animator;


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

    }
}
