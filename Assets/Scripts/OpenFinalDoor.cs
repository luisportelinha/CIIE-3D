using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseFinalDoor : MonoBehaviour
{
    public float riseSpeed = 1f;
    public float riseDistance = 5f;
    public bool isOpen = false;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        isOpen = false;
        closedPosition = transform.position;
        openPosition = new Vector3(transform.position.x, transform.position.y + riseDistance, transform.position.z);
    }
    
    public void changeDoorState()
    {
        isOpen = !isOpen;
    }
    
    void Update()
    {
       if(isOpen)
       {
        transform.position = Vector3.MoveTowards(transform.position, openPosition, riseSpeed * Time.deltaTime);
       } 
         else
         {
        transform.position = Vector3.MoveTowards(transform.position, closedPosition, riseSpeed * Time.deltaTime);
         }
    }
}
