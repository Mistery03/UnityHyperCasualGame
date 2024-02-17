using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cardSpawner : MonoBehaviour
{
    public UnityEvent UpgradeFuel, UpgradeSpeed, UpgradeFireRate;
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

        int index = Random.Range(0, ArrayObject.Length);
        CopyOfObject = Instantiate(ArrayObject[index], transform);
        CopyOfObject.GetComponent<cardUpgrades>().OnSelected.AddListener(invokeEvents);
       
        
        Destroy(CopyOfObject, 15f);
    }

    public void invokeEvents()
    {
    
        
      
        if(CopyOfObject.tag == "fuel")
            UpgradeFuel.Invoke();
        else if (CopyOfObject.tag == "speed")
            UpgradeSpeed.Invoke();
        else if(CopyOfObject.tag == "fireRate")
            UpgradeFireRate.Invoke();

    

    }
}
