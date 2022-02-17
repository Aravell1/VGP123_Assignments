using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{

    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.gameObject.tag == "Enemy" && Player.GetComponent<PlayerFire>().whipToggle == true)
        {
            if ((gameObject.tag == "LeftWhip" && Player.GetComponent<SpriteRenderer>().flipX == true) || (gameObject.tag == "RightWhip" && Player.GetComponent<SpriteRenderer>().flipX == false))
            {
                Debug.Log("Collided with" + collision.gameObject.name);
                collision.gameObject.GetComponent<ZombieAI>().deathItemDrop();
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Candle" && Player.GetComponent<PlayerFire>().whipToggle == true)
        {
            if ((gameObject.tag == "LeftWhip" && Player.GetComponent<SpriteRenderer>().flipX == true) || (gameObject.tag == "RightWhip" && Player.GetComponent<SpriteRenderer>().flipX == false))
            {
                Debug.Log("Collided with" + collision.gameObject.name);
                collision.gameObject.GetComponent<SpawnPickups>().deathItemDrop();
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Blocks" && Player.GetComponent<PlayerFire>().whipToggle == true)
        {
            if ((gameObject.tag == "LeftWhip" && Player.GetComponent<SpriteRenderer>().flipX == true) || (gameObject.tag == "RightWhip" && Player.GetComponent<SpriteRenderer>().flipX == false))
            {
                Debug.Log("Collided with" + collision.gameObject.name);
                Destroy(collision.gameObject);
            }
        }
    }
}
