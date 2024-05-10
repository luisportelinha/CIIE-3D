using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private bool fase2 = false;
    private bool hasLongAttack;
    private int ataque;
    private Vector3 posicionInicial;

    public InstanceGC gamecontroller;

    private float rangoAtaqueLargoMinimo = 8;
    private float rangoAtaqueLargo = 10;
    private float anguloAtaqueLargo = 25.0f;

    void Start(){
        base.Start();
        // Configuraciones específicas de boss
        rangoVision = 12; 
        rangoAgresividad = 16;
        velocidadCorrer = 5f;
        rangoAtaque = 2;
        anguloVision = 120.0f;
        anguloAtaque = 90.0f;
        damage = 100;
        health = 1000;
        currentHealth = health;
        healthBar.SetMaxHealth(health);
        posicionInicial = transform.position;
    }

    protected override void patrullar(){                                    //El boss al patrullar vuelve a su casilla inicial si pierde el aggro.
        agente.enabled = true;
        animator.SetBool("run", true);
        agente.SetDestination(posicionInicial);

        if (!agente.pathPending && agente.remainingDistance <= agente.stoppingDistance) {
            // Si el enemigo ha llegado a su destino
            animator.SetBool("run", false);
            animator.SetBool("meeleAttack", false);
            animator.SetBool("rangedAttack", false);
            animator.SetBool("combat", false);
            agente.enabled = false;
        }
    }

    protected override void setRun(){
        animator.SetBool("meeleAttack", false);
        animator.SetBool("rangedAttack", false);
        animator.SetBool("run", true);
    }

    protected void atacarLargo(){
        atacando = true;

        animator.SetBool("run", false);
        animator.SetBool("meeleAttack", false); 
        animator.SetBool("rangedAttack", true);
    }

    protected void selectMeele(){                                                 //Se elige de manera aleatoria el ataque a meele, en fase 2 hay mas ataques
        if(!fase2){
            ataque = Random.Range(0,2);
        }else{
            ataque = Random.Range(0,4);
        }
        animator.SetInteger("typemeele", ataque);
    }

    protected override void atacar(){
        atacando = true;
        selectMeele();

        animator.SetBool("run", false);
        animator.SetBool("rangedAttack", false); 
        animator.SetBool("meeleAttack", true); 
        
    }

    private bool tryLongAttack(float distancia, float angleToTarget){               //Si el enemigo se encuentra en una distancia de ataque largo correcta y angulo correcto, se ataca largo
        if ((distancia <= rangoAtaqueLargo && angleToTarget <= anguloAtaqueLargo)
                && (distancia > rangoAtaqueLargoMinimo)) { 

            ataque = Random.Range(0,10);
            if (ataque > 6){
                atacarLargo();
                return true;
            }
        }
        return false;
    }

    private bool tryMeeleAttack(float distancia, float angleToTarget){              //Si el enemigo se encuentra en una distancia de ataque correcta y angulo correcto, se ataca
        if (distancia <= rangoAtaque && angleToTarget <= anguloAtaque) { 
            atacar();
            return true;
        }
        return false;
    }
    
    protected override void comprobarAtaque() {
        // Verifica si la animación de ataque está activa y si ha terminado 
        if (atacando && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f &&
                Vector3.Distance(transform.position, target.transform.position) > rangoAtaque + 0.1f) {
                    

            animator.SetBool("meeleAttack", false); 

            if (Vector3.Distance(transform.position, target.transform.position) <= rangoVision){       // Si al acabar de golpear al objetivo está a rango de vision, se activa run
                animator.SetBool("run", true);
            }
        }
        if (atacando && animator.GetCurrentAnimatorStateInfo(0).IsName("Taunt") && 
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f &&
                Vector3.Distance(transform.position, target.transform.position) > rangoAtaque + 0.1f) {

            animator.SetBool("meeleAttack", false); 

            if (Vector3.Distance(transform.position, target.transform.position) <= rangoVision){        // Si al acabar de golpear al objetivo está a rango de vision, se activa run
                animator.SetBool("run", true);
            }
        }
    }

    protected override void Comportamiento_Enemigo(){
        float distancia = Vector3.Distance(transform.position, target.transform.position);
        Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

        if(!sinCombate && distancia > rangoAgresividad){                                     //Si se está en combate pero el jugador no está en rango de agresividad, se resetea
            sinCombate = true;
            animator.SetBool("combat", false);
        }

        if ((distancia > rangoVision || angleToTarget > anguloVision) && sinCombate){        // El objetivo está fuera del rango de visión y no está en combate
            patrullar();
        }else{   
            if (sinCombate){
                animator.Play("Idle Fight");
                sinCombate = false;
            }
            animator.SetBool("combat", true);

            if(!tryLongAttack(distancia, angleToTarget)){                                   //Intenta realizar un ataque con rango
                if(!tryMeeleAttack(distancia, angleToTarget)){                              //Intenta realizar un ataque con meele
                    perseguir();                                                            //Si no realiza ningun ataque se persigue y se pone atacando a false
                    atacando = false;
                }
            }

        }  
        if(atacando){                                                                       //Si se está atacando, se desactiva el agente de navegación
            agente.enabled = false;
        }
    }

    public override void TakeDamage(int amount){
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        print($"Recibido {amount} de daño, salud restante: {currentHealth}");
        if ((currentHealth <= health * 0.5) && (fase2 == false)){                           //Cuando la salud actual del jefe baja del 50% por primera vez entra en la fase 2
            fase2 = true;
            damage = 200;
            animator.SetBool("fase2", true);
            animator.Play("Fase 2");
        }
        if (currentHealth <= 0){
            print("enemigo muerto");
            animator.Play("Death");
            Destroy(gameObject, 5f);
            Cursor.lockState = CursorLockMode.None;
            gamecontroller.CambiarEscena(7);
        }
        
    }
}
