using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHouses : MonoBehaviour
{

    public Transform[] spawnPoints; 
    public GameObject[] houses;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform point in spawnPoints)
        {
            if(Random.value < .7f)
            {
                GameObject newHouse = Instantiate(houses[Random.Range(0,houses.Length)]);
                newHouse.transform.position = point.position;
            }
            
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
