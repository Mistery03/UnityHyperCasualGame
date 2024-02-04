using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class spaceshipPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    float _speed, x, y;
    Vector2 _screenBounds;

    public UnityEvent OnRunOutOfFuel, OnHit, OnLevelUp;
    public float MaxFuel = 100;
    public float CurrentFuel, FuelFillAmount, DrainAmount;
    public float ScoreAmount = 0;
    public float scoreMultiplier = 1;
    public float totalScore = 0;

    public float MaxLevel = 10;
    public float Currentlevel = 0;
    public float CurrentXp, TargetXp;

    public float damage;

    private float fuelPercentage;

    public FuelBarObject FuelBar;
    public GameManager GameManager;
    public GameObject missile;

    public bool isFreeze, isFiring;


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
        GameManager.totalScore = 0;
        GameManager.setSpaceshipDamage(damage);

    }

    // Update is called once per frame
    void Update()
    {
        if(!isRanOutFuel)
        {
            fuelPercentage = CurrentFuel / MaxFuel;
            GameManager.setSpaceshipFuelLevel(fuelPercentage);

            SpaceshipMovement();


            GameManager.scoreHUD(GameManager.totalScore);

            if(!isFreeze)
                drainFuel(DrainAmount);

            if (CurrentFuel <= 0)
                OnRunOutOfFuel.Invoke();
                

            // Increment the timer
            timeSinceLastSpawn += Time.deltaTime;

            // Check if enough time has passed to spawn a new object
            if (isFiring && timeSinceLastSpawn >= spawnRate)
            {
                shootProjectile();
                timeSinceLastSpawn = 0f; // Reset the timer
            }

            CurrentXp += 1 * Time.deltaTime;

            if(CurrentXp >= TargetXp)
            {
                OnLevelUp.Invoke();
            }
    
        }


    }  

    void SpaceshipMovement()
    {
        // Get the acceleration value along the x-axis
        x = Input.acceleration.x;
        y = Input.acceleration.y;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime;
        transform.position += movement;
        transform.position += new Vector3(x * _speed * Time.deltaTime, y * _speed * Time.deltaTime, 0);
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
      if(collision.gameObject.tag == "fuel") 
      {
            CurrentFuel += FuelFillAmount;

            if (CurrentFuel >= MaxFuel)
                CurrentFuel = MaxFuel;

            Destroy(collision.gameObject);
      }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            OnHit.Invoke();

            Destroy(collision.gameObject);
        }
    }

    public float getFuelLevel()
    {
        return fuelPercentage;
    }

    public void GameOverScreen()
    {
        GameManager.GameOver(GameManager.totalScore);
        isRanOutFuel = true;
        GameManager.LevelHudText.enabled = false;
        GameManager.ScoreHudText.enabled = false;

    }

    public void DamageSpaceShip()
    {
        CurrentFuel -= FuelFillAmount;
        FuelBar.SetFuel(CurrentFuel);
    }

    public void LevelUpSpaceship()
    {
        CurrentXp = CurrentXp - TargetXp;
     
        GameManager.levelHUD(GameManager.currentLevel += 1);
        TargetXp += 10;
        Time.timeScale = 0f;

    }

    public void increaseSpaceShipFuel()
    {
        MaxFuel += 100;
        CurrentFuel = MaxFuel;
        FuelBar.setMaxFuel(MaxFuel);
        FuelBar.SetFuel(CurrentFuel);
    }

  
}
