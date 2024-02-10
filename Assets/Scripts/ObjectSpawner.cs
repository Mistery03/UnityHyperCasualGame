using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]float _maxTime = 3f;
    float _timer;
    public GameObject [] ArrayObject;
    [SerializeField] float _positionRange = 0.45f;
    public UnityEvent OnEyesDestroy;
    GameObject CopyOfObject;


    private void Awake()
    {
        if (_timer > _maxTime)
        {
            SpawnObjects();
            _timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > _maxTime)
        {
            SpawnObjects();
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }

    void SpawnObjects()
    {
     
        int index = Random.Range(0, ArrayObject.Length);
        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-_positionRange, _positionRange),0,0);
        CopyOfObject = Instantiate(ArrayObject[index], spawnPos, Quaternion.identity);

        if (CopyOfObject.tag == "eyes")
            CopyOfObject.GetComponent<eyes>().OnDestroy.AddListener(OnDestroyEvent);

        Destroy(CopyOfObject, 10f);
    }

    void OnDestroyEvent()
    {
        OnEyesDestroy.Invoke();
    }

 
}
