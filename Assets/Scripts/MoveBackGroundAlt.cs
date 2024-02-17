using UnityEngine;

public class MoveBackGroundAlt : MonoBehaviour
{
    public float OriginalSpeed;
    public GameManager GameManager;

    [SerializeField] float _width;
    private Vector2 _startPos;

    // Define fuel level thresholds and corresponding multipliers
    [System.Serializable]
    public struct FuelLevelMultiplier
    {
        public float fuelLevelThreshold;
        public float speedMultiplier;
        public float scoreMultiplier;
    }

    public FuelLevelMultiplier[] fuelLevelMultipliers;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        float fuelLevel = GameManager.getSpaceshipFuelLevel();

        // Initialize multipliers with default values
        float speedMultiplier = 1f;
        float scoreMultiplier = 0.01f;

        // Iterate through the fuel level multipliers
        foreach (FuelLevelMultiplier levelMultiplier in fuelLevelMultipliers)
        {
            // If the current fuel level is greater than or equal to the threshold
            if (fuelLevel >= levelMultiplier.fuelLevelThreshold)
            {
                // Set the speed and score multipliers accordingly
                speedMultiplier = levelMultiplier.speedMultiplier;
                scoreMultiplier = levelMultiplier.scoreMultiplier;

                // No need to continue checking further levels
                break;
            }

            
        }

   
  

        // Set the speed and score multipliers in the game manager
        GameManager.setSpeedMultiplier(speedMultiplier);
        GameManager.setScoreMultiplier(scoreMultiplier);

        // Calculate the current speed based on the original speed and speed multiplier
        float currentSpeed = OriginalSpeed * Time.fixedDeltaTime * GameManager.getSpeedMultiplier();

        // Calculate the new position based on the current speed and time
        float newPosition = Mathf.Repeat(Time.time * currentSpeed, _width);

        // Move the background
        transform.position = _startPos + Vector2.down * newPosition;

       
        // Update the current speed in the game manager
        GameManager.setCurrentSpeed(currentSpeed);
        GameManager.setOriginalSpeed(OriginalSpeed);
    }

    public void increaseSpeed(float speed)
    {
        OriginalSpeed += speed;
    }

    public void decreaseSpeed(float speed)
    {
        OriginalSpeed -= speed;
    }

    public float getCurrentSpeed() { return OriginalSpeed; }
}