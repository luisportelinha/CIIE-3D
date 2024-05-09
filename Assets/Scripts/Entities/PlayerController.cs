using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Transform playerCam;
    public Transform orientation;
    Rigidbody rb;
    public Animator animator;

    [Header("Movement")]
    public bool lockLook;
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;


    public float energy;
    public int health, regen, currentHealth;
    int maxHealth;
    bool fighting;
    bool isTakingDamage;

    public HealthBar healthBar;
    public Vector3 inputVector;
    public float moveSpeed;
    public float baseSpeed = 20;
    private float startBaseSpeed;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;


    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    float horizontalInput;
    float verticalInput;
    public float x, y;
    bool sprinting;
    //air control
    public float airForwardForce;

    Vector3 moveDirection;
    
    [Header("Doors Check")]
    public float radioApertura = 10f; // Radio de apertura de las puertas
    public LayerMask maskDoors;

    [Header("Portals Check")]
    public LayerMask maskPortal;

    public GameController gameController;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startBaseSpeed = baseSpeed;
        maxHealth = health;
        currentHealth = health;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;


        animator.SetFloat("VelX", Input.GetAxis("Horizontal"));
        animator.SetFloat("VelY", Input.GetAxis("Vertical"));

        if (Input.GetKey("space") && grounded && readyToJump)
        {
            animator.Play("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            readyToJump = false; // Set the flag to false to prevent immediate subsequent jumps
            Invoke("ResetJumpCooldown", 1f); // Schedule resetting the flag after 1 second
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Detectar todas las puertas dentro del radio de apertura
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radioApertura, maskDoors);

            // Iterar sobre las puertas detectadas
            foreach (Collider doorCollider in hitColliders)
            {
                if (doorCollider.CompareTag("Door"))
                {
                    // Cambiar el estado de la puerta
                    doorCollider.GetComponent<RotateDoor>().changeDoorState();
                } else if (doorCollider.CompareTag("FinalDoor"))
                {
                    // Cambiar el estado de la puerta
                    doorCollider.GetComponent<RiseFinalDoor>().changeDoorState();
                }
            }
        }

    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        print($"Recibido {amount} de daño, salud restante: {currentHealth}");
        if (currentHealth <= 0){
            Die();
        }
    }
    
    void Die(){
        print("El jugador ha muerto.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal")){
            gameController.PasarEscena();
        }
        if (other.CompareTag("Damagexs40") && !isTakingDamage)
        {
            StartCoroutine(TakeDamageOverTime(other));
        }

        if(other.CompareTag("weapon")){  
            Enemy enemy = other.GetComponentInParent<Enemy>(); 
            if (enemy != null) {
                TakeDamage(enemy.damage);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Damagexs10"))
        {
            StopCoroutine(TakeDamageOverTime(other));
            isTakingDamage = false;
        }

        if (other.CompareTag("Damagexs40"))
        {
            StopCoroutine(TakeDamageOverTime(other));
            isTakingDamage = false;
        }
    }

    IEnumerator TakeDamageOverTime(Collider other)
    {
        isTakingDamage = true;
        while (isTakingDamage)
        {
            if (other.CompareTag("Damagexs10"))
            {
                currentHealth -= 10;
                healthBar.SetHealth(currentHealth);
            }

            if (other.CompareTag("Damagexs40"))
            {
                currentHealth -= 40;
                healthBar.SetHealth(currentHealth);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    public void DashInDirection(Vector3 dir, float force)
    {
        rb.AddForce(dir * force, ForceMode.Impulse);
    }


}
