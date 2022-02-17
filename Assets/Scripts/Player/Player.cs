using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Player : MonoBehaviour
{
    public bool verbose = false;
    public bool isGrounded;
    public UnityEngine.UI.Text health;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    int _score = 0;
    int _lives = 0;
    public int hp = 10;

    [SerializeField]
    public bool dagger = false;

    public bool hurt = false;
    public float timer;
    
    [SerializeField]
    float speed;

    [SerializeField]
    int jumpForce;

    [SerializeField]
    float groundCheckRadius;

    [SerializeField]
    LayerMask isGroundLayer;

    [SerializeField]
    Transform groundCheck;

    //bool coroutineRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 3.0f;
            if (verbose)
            {
                Debug.Log("Speed changed to default value of 3");
            }
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
            if (verbose)
            {
                Debug.Log("Jump Force changed to default value of 300");
            }
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.05f;
            if (verbose)
            {
                Debug.Log("Ground Check Layer changed to default value of 0.05");
            }
        }

        if (!groundCheck)
        {
            groundCheck = transform.GetChild(0);
            if (verbose)
            {
                if (groundCheck.name == "GroundCheck")
                {
                    Debug.Log("Ground Check Found and Assigned");
                }
                else
                {
                    Debug.Log("Manually set ground check as it could not be found!");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        float hInput = Input.GetAxisRaw("Horizontal");
        

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        if (isGrounded && Input.GetButtonDown("Jump") && !hurt)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 moveDir = new Vector2(hInput * speed, rb.velocity.y);
        rb.velocity = moveDir;

        anim.SetFloat("xVel", Mathf.Abs(hInput));
        anim.SetBool("isGrounded", isGrounded);

        if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX)
        {
            sr.flipX = !sr.flipX;
        }

        if (hurt)
        {
           // Debug.Log("Timer started");
            timer += Time.deltaTime;
        }

        if (timer >= 1.5f)
        {
            //Debug.Log("Timer Ended");
            hurt = false;
            anim.SetBool("damage", false);
            speed = 3f;
            timer = 0;
        }

        if (hp <= 0)
        {
            SceneManager.LoadScene("Level1");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Projectile will not destroy itself on the floor, but will on the wall
        if (collision.gameObject.tag == "Enemy" && !hurt)
        {
            //Debug.Log("Damage Taken");
            hurt = true;
            anim.SetBool("damage", true);
            GameObject.Find("Life" + hp).GetComponent<Image>().enabled = false;
            hp--;
            speed = 0f;
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

}



