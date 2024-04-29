using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aleatorios : MonoBehaviour
{
    private int movimiento;
    private float random;
    public AnimationClip walkA;
    public AnimationClip idleA;
    
    void Start () {
        movimiento = Random.Range(0,4);
    }

    void Update(){
        if (movimiento == 0){
            GetComponent<Animation>().CrossFade(idleA.name);
            random += 70*Time.deltaTime;
        }
        else if (movimiento == 1){
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            GetComponent<Animation>().CrossFade(walkA.name);
            random += 70*Time.deltaTime;
        }

        else if (movimiento == 2){
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            transform.Rotate(Vector3.up * 20 * Time.deltaTime);
            GetComponent<Animation>().CrossFade(walkA.name);
            random += 70*Time.deltaTime;
        }

        else if (movimiento == 3){
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            transform.Rotate(Vector3.down* 20 * Time.deltaTime);
            GetComponent<Animation>().CrossFade(walkA.name);
            random += 70*Time.deltaTime;
        }

        if (random >= 100){
            movimiento = Random.Range(0,4);
            random = 0;
        }   
    }
}
