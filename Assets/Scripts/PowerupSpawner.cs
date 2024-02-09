using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerupSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]float _maxTime = 3f;
    float _timer;
    public GameObject [] ArrayObject;
    [SerializeField] float _positionRange = 0.45f;
    public UnityEvent OnMeteorDestroy;
    GameObject CopyOfObject;


    private void Awake()
    {
        if (_timer > _maxTime)
        {
            SpawnFuel();
            _timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > _maxTime)
        {
            SpawnFuel();
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }

    void SpawnFuel()
    {
     
        int index = Random.Range(0, ArrayObject.Length);
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-_positionRange, _positionRange),0,0);
        CopyOfObject= Instantiate(ArrayObject[index], spawnPos, Quaternion.identity);

       
            

        Destroy(CopyOfObject, 10f);
    }

 
}
