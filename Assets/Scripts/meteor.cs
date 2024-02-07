using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class meteor : MonoBehaviour
{
    // Start is called before the first frame update
    public float CurrentMeteorHealth;
    public float MaxMeteorHealth;
    public float meteorHealthPercentage;
    public float SpeedMultiplier;
    public float speed;
    public Rigidbody2D meteorite;
    public Rigidbody2D fuel;

    public UnityEvent OnHit, OnDestroy;

 
    void Start()
    {
        CurrentMeteorHealth = MaxMeteorHealth;
        meteorHealthPercentage = CurrentMeteorHealth / MaxMeteorHealth;
    

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "missile")
        {
            Destroy(collision.gameObject);
            OnHit.Invoke();
        }else if(collision.gameObject.tag == "Player")
            OnHit.Invoke();
        
            


      
        
    }

    public void ScaleObject(float healthPercentage)
    {
        // Ensure the percentage is within valid range (0 to 1)
        healthPercentage = Mathf.Clamp01(healthPercentage);

        // Define the initial scale and the minimum scale you want to reach
        Vector3 initialScale = new Vector3(5f,5f,5f);
        Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Interpolate between the initial scale and the minimum scale based on health percentage
        Vector3 newScale = Vector3.Lerp(initialScale, minScale, 1 - healthPercentage);

        // Apply the new scale to the object
        transform.localScale = newScale;
    }

   

    // Update is called once per frame
    void Update()
    {
        if (meteorHealthPercentage < 0.2f)
        {
            Destroy(GameObject.FindGameObjectWithTag("Meteor"));
           

            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-1, 1), 0, 0);
            Rigidbody2D CopyOfObject = Instantiate(fuel, spawnPos, Quaternion.identity);
            OnDestroy.Invoke();
            Destroy(CopyOfObject, 10);
            

        }

        transform.position += new Vector3(0, -1, 0) * speed * SpeedMultiplier * Time.deltaTime; 
        
    }

    public void DamageMeteor(int dmg)
    {
        CurrentMeteorHealth -= dmg;
        meteorHealthPercentage = CurrentMeteorHealth /MaxMeteorHealth;
        ScaleObject(meteorHealthPercentage);
    }


}
