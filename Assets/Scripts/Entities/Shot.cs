using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    
    public int damage;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
