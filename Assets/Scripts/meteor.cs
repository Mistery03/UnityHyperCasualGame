using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public UnityEvent OnDestroy;
    public UnityEvent<Collision2D> OnHit;


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
            OnHit.Invoke(collision);
        }
        
            


      
        
    }

    public void ScaleObject(float healthPercentage)
    {
        // Ensure the percentage is within valid range (0 to 1)
        healthPercentage = Mathf.Clamp01(healthPercentage);

        // Define the initial scale and the minimum scale you want to reach
        Vector3 initialScale = new Vector3(2f,2f,2f);
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
            Destroy(gameObject);
            //Destroy(GameObject.FindGameObjectWithTag("Meteor"));
            //Destroy(GameObject.FindGameObjectWithTag("ColdMeteor"));

            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-1, 1), 0, 0);
            Rigidbody2D CopyOfObject = Instantiate(fuel, spawnPos, Quaternion.identity);
            OnDestroy.Invoke();
            Destroy(CopyOfObject, 10);
            

        }
        transform.Rotate(new Vector3(0,0, -20) * Time.deltaTime);
        transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime; 
        
    }

    public void DamageMeteor(Collision2D collidedObject)
    {
        
        CurrentMeteorHealth -= collidedObject.gameObject.GetComponent<projectile>().MissileDamage;
        meteorHealthPercentage = CurrentMeteorHealth /MaxMeteorHealth;
        ScaleObject(meteorHealthPercentage);
    }


}
