using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected int rutina;
    protected float crono;
    protected Animator animator;
    protected Quaternion angulo;
    protected float grado;
    protected bool atacando = false;
    protected bool sinCombate = true;

    public int rangoVision = 5;
    public int rangoAgresividad = 8;
    public float velocidad = 1;
    public float velocidadCorrer = 2;
    public float rangoAtaque = 1;
    public float anguloVision = 180.0f;
    public float anguloAtaque = 15.0f;
    public int damage = 5;
    public int health = 100;
    protected int currentHealth;
    
    public HealthBar healthBar;
    
    public GameObject target;
    public NavMeshAgent agente;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        agente = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.Find("Player");
        currentHealth = health;
    }

    protected virtual void patrullar(){
        agente.enabled = false;
        animator.SetBool("run", false);

        crono += 1 * Time.deltaTime;
        if (crono >= 4){
            rutina = Random.Range(0,2);
            crono = 0;
        }
        if (rutina == 0){
            animator.SetBool("walk", false);
        }else{
            grado = Random.Range(0, 360);
            angulo = Quaternion.Euler(0, grado, 0);
            animator.SetBool("walk", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")){
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            }
        }
    }

    protected virtual void setRun(){
        animator.SetBool("walk", false);
        animator.SetBool("attack", false);
        animator.SetBool("run", true);
    }

    public void perseguir(){
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        agente.enabled = true;
        
        setRun();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run")){
            agente.SetDestination(target.transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1);
        }
    }

    protected virtual void atacar(){
        atacando = true;   
    }

    protected virtual void comprobarAtaque() {
        // Verifica si la animación de ataque está activa y si ha terminado
        if (atacando && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && 
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f &&
                Vector3.Distance(transform.position, target.transform.position) >= rangoAtaque) {
            atacando = false; 
            animator.SetBool("attack", false); 

            if (Vector3.Distance(transform.position, target.transform.position) <= rangoVision){   // Si al acabar de golpear al objetivo está a rango de vision, se activa run
                animator.SetBool("run", true);
            }
        }
    }

    protected virtual void Comportamiento_Enemigo(){
        float distancia = Vector3.Distance(transform.position, target.transform.position);
        Vector3 dirToTarget = (target.transform.position - transform.position).normalized;
        float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

        if(!sinCombate && distancia > rangoAgresividad){  //Si se está en combate pero el jugador no está en rango de agresividad, se resetea
            sinCombate = true;
        }

        if ((distancia > rangoVision || angleToTarget > anguloVision) && sinCombate){        // El objetivo está fuera del rango de visión y no está en combate
            patrullar();
        }
        else{   
            sinCombate = false;
            if (distancia > rangoAtaque || angleToTarget > anguloAtaque) {  // El objetivo está visible pero fuera del rango de ataque
                if (!atacando) perseguir();
            }
            else{
                atacar();
            }
        }  
        if(atacando){
            agente.enabled = false;
        }
    }

    public virtual void TakeDamage(int amount){                           //Funcion encargada de manejar el recibir daño de distintas fuentes
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);
        print($"Recibido {amount} de daño, salud restante: {currentHealth}");
        if (currentHealth <= 0){
            Die();
        }
    }


    protected void Die(){                                                   //Activa el estado de death y destruye el cuerpo al pasar 5 segundos
        print("enemigo muerto");
        animator.Play("Death");
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
        comprobarAtaque();
    }

    
}
