using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrCamera : MonoBehaviour
{

    Vector3 offset;                             // vector entre player i càmera
    Transform player;                           // apunta al player
    bool esquerra = false, dreta = false;       // si camera es mou esq o dreta
    [SerializeField] float elasticCam = 0.07f;  // suavitza moviment càmera
    [SerializeField] float margeEsq = 0.3f, margeDret = 0.6f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // si el player sobrepassa marge
        if (Camera.main.WorldToViewportPoint(player.position).x < margeEsq)
        {
            if (!esquerra)       // si comencem scroll esq, calculem offset
            {
                offset = player.position - transform.position;
                esquerra = true;
            }
        }
        else esquerra = false;

        if (Camera.main.WorldToViewportPoint(player.position).x > margeDret)
        {
            if (!dreta)          // si comencem scroll dreta, calculem offset
            {
                offset = player.position - transform.position;
                dreta = true;
            }
        }
        else dreta = false;

        if (esquerra || dreta)  // si hi ha scroll a esquerra o dreta, seguim al player
        {
            float x = Mathf.Lerp(transform.position.x, player.position.x - offset.x, elasticCam);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}
