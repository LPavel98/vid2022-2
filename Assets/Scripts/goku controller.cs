using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gokucontroller : MonoBehaviour
{
    Rigidbody2D rb; 
    SpriteRenderer sr;
    public float velocity = 10;
    private float defaultGravity;
    
    private Vector2 direction;
    private bool tieneNube = false;
    Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        defaultGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        direction = new Vector2(x, y);
        Run();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        if (Input.GetKey(KeyCode.UpArrow) && tieneNube )
        {
            rb.velocity = new Vector2(rb.velocity.x, velocity);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && tieneNube)
        {
            rb.velocity = new Vector2(rb.velocity.x, -velocity);
        }
    }

    private void Run(){
        
        rb.velocity = new Vector2 (direction.x * velocity, rb.velocity.y);
        sr.flipX = direction.x < 0;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "nube"){
            Destroy(other.gameObject);
            rb.gravityScale = 0;
            tieneNube = true;
            animator.SetInteger("Estado", 1);
        } 
    }
    void OnCollisionEnter2D (Collision2D other){
        if (other.gameObject.name == "suelo"){
            rb.gravityScale = defaultGravity;
            tieneNube = true;
            animator.SetInteger("Estado", 0);
        } 
    }


}
