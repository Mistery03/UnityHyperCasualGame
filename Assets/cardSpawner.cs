using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cardSpawner : MonoBehaviour
{
    public UnityEvent UpgradeFuel, UpgradeLevel;
    public GameObject[] ArrayObject;
    GameObject CopyOfObject;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCard()
    {

        int indexer = Random.Range(0, ArrayObject.Length);
        CopyOfObject = Instantiate(ArrayObject[1], transform);
        CopyOfObject.GetComponent<cardUpgrades>().OnSelected.AddListener(invokeEvents);
       
        
        //Destroy(CopyOfObject, 15f);
    }

    public void invokeEvents()
    {
    
        
      
        if(CopyOfObject.tag == "fuel")
            UpgradeFuel.Invoke();

    }
}
