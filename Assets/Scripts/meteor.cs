using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{
    // Start is called before the first frame update
    public float CurrentMeteorHealth;
    public float MaxMeteorHealth;
    public float meteorHealthPercentage;
    public float SpeedMultiplier;
    public Rigidbody2D meteorite;
    public Rigidbody2D fuel;
    void Start()
    {
        CurrentMeteorHealth = MaxMeteorHealth;
        meteorHealthPercentage = CurrentMeteorHealth / MaxMeteorHealth;
        meteorite.gravityScale = 1;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "missile")
            Destroy(collision.gameObject);

        CurrentMeteorHealth -= 50;
        meteorHealthPercentage = CurrentMeteorHealth / MaxMeteorHealth;
        ScaleObject(meteorHealthPercentage);
    }

    void ScaleObject(float healthPercentage)
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

    /*void changeGravityScale()
    {


        // Define the initial scale and the minimum scale you want to reach
        float initialGravityScale = 1f;
        float minScale = 1f;

        // Interpolate between the initial scale and the minimum scale based on health percentage
        float newMultiplier = Mathf.Lerp(initialGravityScale, SpeedMultiplier, 5);

        // Apply the new scale to the object
        meteorite.gravityScale = newMultiplier;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (meteorHealthPercentage < 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Meteor"));
           

            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-1, 1), 0, 0);
            Rigidbody2D CopyOfObject = Instantiate(fuel, spawnPos, Quaternion.identity);
            Destroy(CopyOfObject, 10);
            
            

            
           
        }


        meteorite.gravityScale = SpeedMultiplier;
    }
}
