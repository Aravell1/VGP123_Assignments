using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickups : MonoBehaviour
{
   enum CollectibleType
    {
        POWERUP,
        LIFE
    }

    [SerializeField] CollectibleType curCollectible;
    public int addLife;

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player curPlayerScript = collision.gameObject.GetComponent < Player > ();
            switch (curCollectible)
            {
                case CollectibleType.POWERUP:
                    curPlayerScript.dagger = true;
                    break;
                case CollectibleType.LIFE:
                    if (curPlayerScript.hp < 10)
                    {
                        curPlayerScript.hp += addLife;
                        if (curPlayerScript.hp > 10)
                        {
                            curPlayerScript.hp = 10;
                        }
                        GameObject.Find("Life" + curPlayerScript.hp).GetComponent<Image>().enabled = true;
                        GameObject.Find("Life" + (curPlayerScript.hp - 1)).GetComponent<Image>().enabled = true;
                    }
                    break;
            }
            Destroy(gameObject);
        }
    }
}
