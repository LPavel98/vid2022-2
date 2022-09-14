using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip jumpClip;
    public AudioClip bulletClip;
    public AudioClip powerupClip;

    public float JumpForce = 5;
    public GameObject bullet;
    public float velocity = 10;
    Rigidbody2D rb; 
    SpriteRenderer sr;
    Animator animator;
    const int ANIMATION_QUIETO = 0;
    const int ANIMATION_CORRER = 1;

    private GameManagerController gameManager;
    AudioSource audioSource;
    bool puedeSaltar = true;


    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManagerController>();
        Debug.Log("Iniciamos script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity.x;
        //rb.velocity.y;

        //GetKey
        //GetKeyUp
        //GetKeyDown

        
        
        Debug.Log("Puede saltar"+puedeSaltar.ToString());
        if (Input.GetKeyUp(KeyCode.G) && sr.flipX == true)
        {
            var bulletPosition = transform.position + new Vector3(-3,0,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetLeftDirection();
            audioSource.PlayOneShot(bulletClip);
        }
        else if (Input.GetKeyUp(KeyCode.G) && sr.flipX == false)
        {
            var bulletPosition = transform.position + new Vector3(3,0,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller = gb.GetComponent<BulletController>();
            controller.SetRightDirection();
            audioSource.PlayOneShot(bulletClip);
        }
        else if (Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(velocity, rb.velocity.y);  
            sr.flipX = false;
            ChangeAnimation(ANIMATION_CORRER);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
            sr.flipX = true;
            ChangeAnimation(ANIMATION_CORRER);
        }
                

        else if (Input.GetKeyUp(KeyCode.Space) && puedeSaltar)
        {
            rb.AddForce(new Vector2(0,JumpForce), ForceMode2D.Impulse);
            puedeSaltar = false;
            audioSource.PlayOneShot(jumpClip);
        }
        
        else if (gameManager.livesText.text == "GAME OVER")
                {
                    ChangeAnimation(ANIMATION_CORRER);
                    Debug.Log("Estas muerto");
                }
       
       
            

    }  

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Puede saltar");
        puedeSaltar = true;
            if (other.gameObject.name == "Hongo")
            {
                Debug.Log("Estas muerto");
                gameManager.PerderVida();
                transform.localScale = new Vector3(0.7F, 0.7F, 1);
                audioSource.PlayOneShot(powerupClip);

            }
            if (other.gameObject.tag == "Enemy")
            {
                gameManager.PerderVida();
                transform.localScale = new Vector3(0.3F, 0.3F, 1);
               //Destroy(other.gameObject);
                //gameManager.GanarPuntos(10);
            }  
            if (other.gameObject.name == "Escalera")
            {
                gameManager.PerderVida();
               
// rb.velocity = new Vector2(1, rb.velocity.x);
                
               //Destroy(other.gameObject);
                //gameManager.GanarPuntos(10);
                
            rb.velocity = new Vector2 (1, 3);
            ChangeAnimation(ANIMATION_QUIETO);
        
                
                
            }  
            
    }
   

    private void ChangeAnimation(int animation){
        animator.SetInteger("Estado", animation);

    }

}
