using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Transform rb;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
   
    }

    // Update is called once per frame
    private void Update()
    {
        //rotate orientation
        Vector3 viewDir = player.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(inputDir != Vector3.zero)
        {
            playerObj.forward = inputDir.normalized;
        }
        
    }
}
