using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrDetection : MonoBehaviour
{
    ScrPhantom father;
    // Start is called before the first frame update
    void Start()
    {
        father = GetComponentInParent<ScrPhantom>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                print("Player on shight");
                father.OnTriggerEnter2DChild();
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                print("Nothing in shight");
                
                father.OnTriggerExit2DChild();
                break;
            default:
                break;
        }
    }
}
