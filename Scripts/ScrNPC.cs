using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrNPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            if (collision.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1,1,1);
            }
            else 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        
        }
    }
}
