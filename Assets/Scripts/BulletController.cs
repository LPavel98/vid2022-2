using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour

{
    public float velocity = 20;
    Rigidbody2D rb;
    float realVelocity;
    
   
    public void SetRightDirection(){
        realVelocity = velocity;
    }
     public void SetLeftDirection(){
        realVelocity = -velocity;
    }
    void Start()
    {
            rb = GetComponent<Rigidbody2D>();   
            Destroy(this.gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(realVelocity, 0);
    }

    void OnCollisionEnter2D(Collision2D other) {
            Destroy(this.gameObject); 
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
                
            }
              
    }
   
}
