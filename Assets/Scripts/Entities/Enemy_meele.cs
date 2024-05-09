using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_meele : Enemy
{


    protected override void Start()  // Sobreescritura del Start
    {
        base.Start();  
        // Configuraciones específicas de Enemy_meele
        rangoVision = 8; 
        rangoAgresividad = 12;
        velocidadCorrer = 2;
        rangoAtaque = 1;
        anguloVision = 90.0f;
        anguloAtaque = 45.0f;
        damage = 20;
        health = 200;
    }

    protected override void atacar(){
        atacando = true;

        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        animator.SetBool("attack", true); 
    }

    
}
