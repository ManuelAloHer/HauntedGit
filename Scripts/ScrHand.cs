using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrHand : MonoBehaviour
{
    Animator anim;
    [SerializeField]GameObject recupoint;
    bool atacking = false;
    ScrPhantom phantom;
    Vector3 atktarg;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        phantom = GetComponentInParent<ScrPhantom>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, atktarg,  // desplazamos
                                            Time.deltaTime * phantom.speed);
            if (transform.position == atktarg) 
            {
                print("Aqui tengo que volver");
                atacking = false;
            }

        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, recupoint.transform.position,  // desplazamos
                                               Time.deltaTime * 4 *phantom.speed);
            FinishAtack(atacking);
        }

        
    }
    public void HandAtack(Vector3 objective)
    { 
        atacking = true;
        atktarg = objective;
    }
    public void FinishAtack(bool atacking)
    {
        phantom.onAtak = atacking;
    }
}
