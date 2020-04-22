using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrFirstTerm : MonoBehaviour
{
    [SerializeField] float speed = 0.6f;
    float initXCam, initXCapa;

    void Start()
    {
        // Posició X inicial de la cámara
        initXCam = Camera.main.transform.position.x;
        initXCapa = transform.localPosition.x;
    }
    void Update()
    {
        float x = (initXCam - Camera.main.transform.position.x); // si x > 0, la camara s'està movent => desplaçar capa
        transform.localPosition = new Vector3(initXCapa + x * speed, transform.localPosition.y, transform.localPosition.z);
    }
}
