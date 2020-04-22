using UnityEngine;
using System.Collections;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓN
///         Script utilizado para mover una plataforma según los puntos de control. 
///         También muestra el camino en la vista escena
/// AUTOR:  Jordi Aguilera
/// DATA:   26/03/2020
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONES
///         1.0: primera versió. 
/// NOTAS: podemos añadir nuevos empty gameobjects hijos que hagan de puntos de control. Tras 
///        hacerlo, deberemos recordar actualizar el array "puntos" desde el inspector
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrPlataforma : MonoBehaviour 
{
    [SerializeField] Transform[] puntos;// array de puntos de control
    [SerializeField] float speed = 2;   // velocidad del movimiento
    float suficienteCerca= .01f;        // a esta distancia considera que ya ha llegado al punto

    int puntoDestino = 1;  // nos dirigiremos del punto 0 al punto 1 
    int direccion = 1;     // 1: ida   -1: vuelta

    public void Start()
    {
        transform.position = puntos[0].position;  // ubicamos plataforma en punto inicial
    }

    public void Update()
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

    // Esta función encarga de dibujar el camino en la vista escena
    public void OnDrawGizmos()
    {
        for (int i = 0; i < puntos.Length - 1; i++)
            Gizmos.DrawLine(puntos[i].position, puntos[i + 1].position);
    }
}