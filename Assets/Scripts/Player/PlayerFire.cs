using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class PlayerFire : MonoBehaviour
{
    public bool verbose = false;
    [SerializeField]
    public bool whipToggle = false;

    SpriteRenderer sr;
    Animator anim;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public float projectileSpeed;
    public Projectile projectilePrefab;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Player = GameObject.Find("Player");

        if (projectileSpeed <= 0)
        {
            projectileSpeed = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            if (verbose)
            {
                Debug.Log("Inspector Values Not Set");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<Player>().dagger == true && Player.GetComponent<Player>().hurt == false && Input.GetButtonDown("Fire1") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            anim.SetTrigger("throw");
        }
        else if (Player.GetComponent<Player>().hurt == false && Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
        }
    }

    public void StartAttack()
    {
        whipToggle = true;
    }
    public void EndAttack()
    {
        whipToggle = false;
    }

    public void FireProjectile()
    {
        if (sr.flipX)
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            temp.speed = -projectileSpeed;
            temp.transform.localScale = new Vector3(-0.61f, 0.61f, 0.61f);  
        }
        else
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            temp.speed = projectileSpeed;
        }
    }
}
