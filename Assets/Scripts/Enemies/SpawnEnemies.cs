using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public ZombieAI[] enemiesPrefabArray;
    public float timer;

    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        timer = Random.Range(0f, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (gameObject.transform.position.x > 33.8)
        {
            if (timer >= 7 && Mathf.Abs(gameObject.transform.position.x - Player.transform.position.x) <= 10 && Mathf.Abs(gameObject.transform.position.x - Player.transform.position.x) >= 3)
            {
                Instantiate(enemiesPrefabArray[Random.Range(0, 3)], transform.position, transform.rotation);
                timer = 0;
            }
        }
        
    }
}
