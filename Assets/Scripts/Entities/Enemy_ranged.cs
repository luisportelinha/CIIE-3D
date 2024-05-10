using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_ranged : Enemy
{   
    public GameObject bullet;
    public Transform spawnBulletPoint;
    public float attackDelay = 0.5f; // Tiempo de espera antes de lanzar el ataque
    public float shotForce = 1500;
    private float lastAttackTime;
    
    protected override void Start(){
        base.Start();  
        // Configuraciones específicas de Enemy_ranged
        rangoVision = 10; 
        rangoAgresividad = 15;
        velocidadCorrer = 1.5f;
        rangoAtaque = 8;
        anguloVision = 120.0f;
        anguloAtaque = 15.0f;
        damage = 20;   
        lastAttackTime = 0;   
        health = 75;
        healthBar.SetMaxHealth(health);
    }

    public void Shoot(){
        GameObject firedBullet = Instantiate(bullet,  spawnBulletPoint.position, spawnBulletPoint.rotation); 
        firedBullet.GetComponent<Rigidbody>().AddForce(spawnBulletPoint.forward * shotForce);
        firedBullet.GetComponent<Shot>().damage = damage;
        Destroy(firedBullet, 3);           
    }

    protected override void atacar(){
        if (Time.time > lastAttackTime) {  // Solo ataca si ha pasado suficiente tiempo desde el último ataque
            atacando = true;
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("attack", true);
            if (atacando && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
                Shoot();
            }
            lastAttackTime = Time.time + attackDelay;        
        }
    }
    
    protected override void comprobarAtaque() {
        // Verifica si la animación de ataque está activa y si ha terminado
        if (atacando && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                Vector3.Distance(transform.position, target.transform.position) >= rangoAtaque) {
            atacando = false; 
            animator.SetBool("attack", false); 

            if (Vector3.Distance(transform.position, target.transform.position) <= rangoVision){   // Si al acabar de golpear al objetivo está a rango de vision, se activa run
                animator.SetBool("run", true);
            }
        }
    }

    void Apuntar(Vector3 targetPosition) {
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected override void Comportamiento_Enemigo(){
        float distancia = Vector3.Distance(transform.position, target.transform.position);
        Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

        if (!sinCombate && distancia > rangoAgresividad) {
            sinCombate = true;
        }

        if ((distancia > rangoVision || angleToTarget > anguloVision) && sinCombate) {
            patrullar();
        }
        else {
            sinCombate = false;
            if (distancia > rangoAtaque || angleToTarget > anguloAtaque) {
                if (!atacando) {
                    perseguir();
                    Apuntar(target.transform.position);  // Añadir orientación mientras persigue
                }
            }
            else {
                Apuntar(target.transform.position);  // Asegurarse de apuntar al jugador antes de atacar
                atacar();
            }
        }  
        if(atacando){
            agente.enabled = false;
        }
    }


}