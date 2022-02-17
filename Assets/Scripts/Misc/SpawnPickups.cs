using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public Pickups[] pickupsPrefabArray;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
    }

    public void deathItemDrop()
    {
        Instantiate(pickupsPrefabArray[Random.Range(0, 2)], transform.position, transform.rotation);
    }
}
