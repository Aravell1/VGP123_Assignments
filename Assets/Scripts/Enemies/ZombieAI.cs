using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{

    [SerializeField]
    float speed;

    Rigidbody2D rb;
    SpriteRenderer sr;

    GameObject Player;
    public Pickups[] pickupsPrefabArray;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        Player = GameObject.Find("Player");

        
        if (Player.transform.position.x < gameObject.transform.position.x)
        {
            speed = -speed;
        }
        else
        {
            sr.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(gameObject.transform.position.x - Player.transform.position.x) > 8)
        {
            if (Player.transform.position.x < gameObject.transform.position.x && speed > 0 || Player.transform.position.x > gameObject.transform.position.x && speed < 0)
            {
                speed = -speed;
                sr.flipX = !sr.flipX;
            }
        }
        
        Vector2 moveDir = new Vector2(speed, rb.velocity.y);
        rb.velocity = moveDir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Item")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
    
    public void deathItemDrop()
    {
        Instantiate(pickupsPrefabArray[Random.Range(0, 3)], transform.position, transform.rotation);
    }
}
