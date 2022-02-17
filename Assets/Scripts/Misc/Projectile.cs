using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <= 0)
        {
            lifetime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (speed < 0)
        {
            sr.flipX = !sr.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Projectile will not destroy itself on the floor, but will on the wall
        if (collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collided with" + collision.gameObject.name);
            collision.gameObject.GetComponent<ZombieAI>().deathItemDrop();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.name != "BoundFloor")
        {
            Destroy(gameObject);
        }
    }

}
