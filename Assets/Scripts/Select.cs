using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    LayerMask mask;
    public float distancia = 3f;

    void Start()
    {
      mask = LayerMask.GetMask("RayCast");   
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
            {
                if (hit.collider.tag == "Door")
                {
                    hit.collider.transform.GetComponent<RotateDoor>().changeDoorState();
                }
            }
        }
    }
}
