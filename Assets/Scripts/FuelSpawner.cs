using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]float _maxTime = 3f;
    float _timer;
    public GameObject [] ArrayObject;
    [SerializeField] float _positionRange = 0.45f;
    void Start()
    {
        SpawnFuel();
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
     
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-_positionRange, _positionRange),0,0);
        GameObject CopyOfObject= Instantiate(ArrayObject[1], spawnPos, Quaternion.identity);

        Destroy(CopyOfObject, 15f);
    }
}
