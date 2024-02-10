using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public int MissileDamage;
    // Start is called before the first frame update
    void Start()
    {
        MissileDamage = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseMissileDamage()
    {
        if (!(MissileDamage >= 1000))
         MissileDamage += 200;
    }
}
