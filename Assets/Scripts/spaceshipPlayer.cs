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

    public AudioSource missleSFX;

    public UnityEvent OnRunOutOfFuel, OnLevelUp, OnHitFreeze, OnFuelFreeze;
    public UnityEvent<Collision2D> OnHit;
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

    public float duration = 10f;


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
        Time.timeScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        if(!isRanOutFuel)
        {
            fuelPercentage = CurrentFuel / MaxFuel;
            GameManager.setSpaceshipFuelLevel(fuelPercentage);

            SpaceshipMovement();


            //GameManager.scoreHUD(GameManager.totalScore);

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

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime;
        transform.position += movement;
        transform.position += new Vector3(x * _speed * Time.deltaTime,0, 0);
    }

    void shootProjectile()
    {
        GameObject copyMissile = Instantiate(missile, transform.position + new Vector3(0,2,0), Quaternion.Euler(0, 0, 90));
        Rigidbody2D rb = copyMissile.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.forward * 100, ForceMode2D.Impulse);
        missleSFX.Play();

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
       
        
       
        if (collision.gameObject.tag == "shoot")
        {
            if(!isFiring)
                StartCoroutine(ActivateShootPowerUp());
        
        
        }
        else if (collision.gameObject.tag == "freeze")
        {
            if (!isFreeze)
                StartCoroutine(ActivateFreeze());

            OnFuelFreeze.Invoke();

        }else
            OnHit.Invoke(collision);

        Destroy(collision.gameObject);

    }

    public float getFuelLevel()
    {
        return fuelPercentage;
    }

    public void GameOverScreen()
    {
        GameManager.GameOver(GameManager.totalScore);
        isRanOutFuel = true;
        GameManager.disableHUDTexts();
        Time.timeScale = 0f;
     

    }

    public void DamageSpaceShip(Collision2D collidedObject)
    {
        if(!isFreeze)
        {
            if(collidedObject.gameObject.tag == "Meteor")
            {
                CurrentFuel -= FuelFillAmount;
                FuelBar.SetFuel(CurrentFuel);
            }else if (collidedObject.gameObject.tag == "eyes")
            {
                CurrentFuel -= FuelFillAmount*2;
                FuelBar.SetFuel(CurrentFuel);
            }
            else if (collidedObject.gameObject.tag == "ColdMeteor")
            {
                
                CurrentFuel -= FuelFillAmount * 0.5f;
                FuelBar.SetFuel(CurrentFuel);
            }


        }

        if (collidedObject.gameObject.tag == "ColdMeteor")
            OnHitFreeze.Invoke();




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
        if(!(MaxFuel >= 1000))
        {
            MaxFuel += 100;
            CurrentFuel = MaxFuel;
            FuelBar.setMaxFuel(MaxFuel);
            
        }

        CurrentFuel = MaxFuel;
        FuelBar.SetFuel(CurrentFuel);
        isFreeze = false;
    }

    IEnumerator ActivateShootPowerUp()
    {
        isFiring = true;
      

        // Add your power-up effects here

        yield return new WaitForSeconds(duration);

    
        isFiring = false;

        // Clean up or remove power-up effects here
    }

    IEnumerator ActivateFreeze()
    {
        float freezeDuration = 2;
        isFreeze = true;
       

        // Add your power-up effects here
        FuelBar.setIsFreeze(isFreeze);

        yield return new WaitForSeconds(freezeDuration);

 
        isFreeze = false;
        FuelBar.setIsFreeze(isFreeze);

        // Clean up or remove power-up effects here
    }


}
