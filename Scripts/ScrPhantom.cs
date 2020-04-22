using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPhantom : MonoBehaviour
{
    [SerializeField] Transform[] puntos;// array de puntos de control
    public float speed = 2;   // velocidad del movimiento
    float suficienteCerca = .01f;        // a esta distancia considera que ya ha llegado al punto

    int puntoDestino = 1;  // nos dirigiremos del punto 0 al punto 1 
    int direccion = 1;     // 1: ida   -1: vuelta

    
    bool lookRight = false;
   
    bool playerDetected = false;
    bool inRange = false;
    public bool onAtak = false;
    public float atkRange = 8f;

    [SerializeField]
    float Damage, pjMaxLife;
    float actuLife;

    Animator anim;
    public GameObject player;
    Vector3 targetPlayer;
    [SerializeField] ScrHand weapon;
    Vector3 curVel, prevLoc = Vector3.zero;

    public void Awake()
    {
        actuLife = pjMaxLife;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    public void Start()
    {
        transform.position = puntos[0].position;  // ubicamos plataforma en punto inicial
    }

    public void Update()
    {
        curVel = (transform.position - prevLoc) / Time.deltaTime;
        prevLoc = transform.position;

            if (!playerDetected)
            {
                
                transform.position = Vector3.MoveTowards(transform.position, puntos[puntoDestino].position,  // desplazamos
                                                 Time.deltaTime * speed);
                float distancia = (transform.position - puntos[puntoDestino].position).sqrMagnitude; // distancia al punto destino
                if (distancia < suficienteCerca)  // si ya hemos llegado
                {
                    if (puntoDestino == 0 || puntoDestino == puntos.Length - 1) direccion = -direccion; // en inicio o final, cambio de dirección
                    puntoDestino += direccion;  // apuntamos al siguiente punto
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position,  // desplazamos
                                        Time.deltaTime * speed);

                if(Vector3.Distance(transform.position, player.transform.position) <= Mathf.Abs(atkRange))
                {
                    inRange = true;
                    if (!onAtak)
                    {
                        targetPlayer = new Vector3(atkRange, player.transform.position.y, player.transform.position.z);
                        Atack();
                        onAtak = true;
                    }

            }
                
                else
                {

                    inRange = false;
                    ///
                    /// Aqui para Arquero
                    /*transform.position = Vector3.MoveTowards(transform.position, player.transform.position,  // desplazamos
                                        Time.deltaTime * speed);*/
                    
                }
            }


        if ((curVel.x < 0 && lookRight) || (curVel.x > 0 && !lookRight))
        {
            FlipSprite();
            
        }

        /*if (inRange) 
        {
            if (!onAtak)
            {
                targetPlayer = player.transform;
                Atack();
                onAtak = true;
            }
                
        }*/
    }

    // Esta función encarga de dibujar el camino en la vista escena
    public void OnDrawGizmos()
    {
        for (int i = 0; i < puntos.Length - 1; i++)
            Gizmos.DrawLine(puntos[i].position, puntos[i + 1].position);
    }
    void Atack() 
    {
        print("Toma perro");
        weapon.HandAtack(targetPlayer);
    }
    void FlipSprite()
    {
        print("Volteo");
        lookRight = !lookRight;
        transform.localScale = new Vector3(transform.localScale.x * -1,
            transform.localScale.y, transform.localScale.z);
    }
    public void OnTriggerEnter2DChild()
    {
        playerDetected = true;
    }
    public void OnTriggerExit2DChild()
    {
        playerDetected = false;
    }
}
