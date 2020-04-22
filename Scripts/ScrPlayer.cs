using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrPlayer : MonoBehaviour
{
    //Variables
    float movement;
    bool lookRight = true;

    [SerializeField] float velo = 3f;
    Rigidbody2D rb;
    [SerializeField] bool touchingTerra;
    [SerializeField] Transform checkTerra;
    [SerializeField] public LayerMask filterLayers;
    float radi = 0.2f; 

    bool sheJumps = false;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] bool jumpsTwice = false;

    Animator anim;
    [SerializeField]
    Text t;
    [SerializeField]
    Canvas globe;
    string [] dialog  = new string [] {"¿Eres un Wookie, no?",
    "¿Sabes donde puedo encontrar una lanzadera?",
    "¿Te gusta el nuevo T-500?",
    "¿Que opinas del Emperador?"};

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        anim.SetFloat("movSpeed", Mathf.Abs(movement));
        if (Input.GetButtonDown("Jump"))
        { 
            sheJumps = true; 
        }
        
        if ((movement <0 && lookRight) || (movement > 0 && !lookRight))
        {
            FlipSprite();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * velo, rb.velocity.y);
        touchingTerra = Physics2D.OverlapCircle(checkTerra.position, radi, filterLayers);
        if (touchingTerra) 
        { 
            jumpsTwice = false; 
        }
        anim.SetBool("isLanded", touchingTerra);
        if (sheJumps && (touchingTerra || jumpsTwice))
        {
            jumpsTwice = !jumpsTwice;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce));
        }
        sheJumps = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FriendFindPlayer") 
        {
            globe.gameObject.SetActive(true);
            t.text = dialog [Random.Range(0, 4)];
        
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "FriendFindPlayer")
        {
            globe.gameObject.SetActive(false);

        }
    }

    void FlipSprite() 
    {
        lookRight = !lookRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 
            transform.localScale.y, transform.localScale.z);
        t.transform.localScale = new Vector3(t.transform.localScale.x * -1,
            t.transform.localScale.y, t.transform.localScale.z);
    }
}
