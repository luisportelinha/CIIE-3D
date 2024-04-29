using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDoor : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float degreeOpen = 60f;
    public float degreeClose = 0f;
    public bool isOpen = false;

    void Start()
    {
        isOpen = false;
    }
    
    public void changeDoorState()
    {
        isOpen = !isOpen;
    }
    
    void Update()
    {
       if(isOpen)
       {
        Quaternion targetRotation = Quaternion.Euler(0, degreeOpen, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
       } 
         else
         {
          Quaternion targetRotation = Quaternion.Euler(0, degreeClose, 0);
          transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
         }
    }
}