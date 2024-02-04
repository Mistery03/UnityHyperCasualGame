using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardSpawner : MonoBehaviour
{
    public GameObject[] ArrayObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCard()
    {

        Vector3 spawnPos = transform.position;
        int indexer = Random.Range(0, ArrayObject.Length);
        GameObject CopyOfObject = Instantiate(ArrayObject[indexer], spawnPos, Quaternion.identity);
        
       

        //Destroy(CopyOfObject, 15f);
    }
}
