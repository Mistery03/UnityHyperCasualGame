using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class spaceshipPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    float _speed, x, y;
    Vector2 _screenBounds;
    public float MaxFuel = 100;
    public float CurrentFuel, FuelFillAmount, DrainAmount;
    public float scoreAmount = 0;
    public float scoreMultiplier = 1;
    public float totalScore = 0;

    private float fuelPercentage;
    private float _timer;

    public FuelBarObject FuelBar;
    public GameManager GameManager;
    public GameObject missile;


    public float spawnRate = 0.1f; // Adjust this value to control the spawn rate

    private float timeSinceLastSpawn = 0f;
    [SerializeField] private GameObject _spaceBackground;

    private bool isRanOutFuel;

    void Start()
    {
        _speed = 10.0f;
        Input.gyro.enabled = true;
        FuelBar.setMaxFuel(MaxFuel);
        FuelBar.SetFuel(CurrentFuel);
        totalScore = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if(!isRanOutFuel)
        {
            fuelPercentage = CurrentFuel / MaxFuel;
            // Get the acceleration value along the x-axis
            x = Input.acceleration.x;

            if (Input.GetKey(KeyCode.A))
                transform.position -= new Vector3(_speed * Time.deltaTime, 0, 0);
            else if (Input.GetKey(KeyCode.D))
                transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);

            // Move the object by adding the acceleration value multiplied by the speed and the time between frames
            transform.position += new Vector3(x * _speed * Time.deltaTime, 0, 0);

           

            GameManager.scoreHUD(totalScore);
            drainFuel(DrainAmount);

            if (CurrentFuel <= 0 && !isRanOutFuel)
            {
                GameManager.GameOver(totalScore);
                isRanOutFuel = true;
              
            }
        }

        // Increment the timer
        timeSinceLastSpawn += Time.deltaTime;

        // Check if enough time has passed to spawn a new object
        if (timeSinceLastSpawn >= spawnRate)
        {
            shootProjectile();
            timeSinceLastSpawn = 0f; // Reset the timer
        }

  

    }  

    void shootProjectile()
    {
        GameObject copyMissile = Instantiate(missile, transform.position + new Vector3(0,2,0), Quaternion.Euler(0, 0, 90));
        Rigidbody2D rb = copyMissile.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.forward * 100, ForceMode2D.Impulse);

        Destroy(copyMissile, 15f);
    }

    void drainFuel(float drain)
    {
        CurrentFuel -= drain * Time.deltaTime;

        FuelBar.SetFuel(CurrentFuel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Test collided");
        CurrentFuel += FuelFillAmount;
        Destroy(collision.gameObject);

    }

    public float getFuelLevel()
    {
        return fuelPercentage;
    }
}
