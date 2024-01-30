using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class MoveBackGround : MonoBehaviour
{
    public float OriginalSpeed;
    public float currentSpeed;
    public float speedMultiplier = 1;
    public spaceshipPlayer spaceship;
    [SerializeField] float _width;

    private Vector2 _startPos;
    void Start()
    {
        
        _startPos = transform.position;
        currentSpeed = OriginalSpeed * Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        float fuelLevel = spaceship.getFuelLevel();
        float targetSpeed;

        if (fuelLevel > 0.45 && fuelLevel <= 0.65)
        {
            speedMultiplier = 2f;
            spaceship.scoreMultiplier = 0.1f;
        }
        else if(fuelLevel > 0.15 && fuelLevel <= 0.45)
        {
            speedMultiplier = 4f;
            spaceship.scoreMultiplier = 1f;
        } 
        else if (fuelLevel <= 0.15)
        {
            speedMultiplier = 16f;
            spaceship.scoreMultiplier = 10f;
        }
        else
        {
            speedMultiplier = 1f;
            spaceship.scoreMultiplier = 0.01f;
        }
      

        float newPosition = Mathf.Repeat(Time.time * currentSpeed * speedMultiplier, _width);

        spaceship.totalScore += spaceship.scoreMultiplier * spaceship.scoreAmount * Time.deltaTime;

        transform.position = _startPos + Vector2.down * newPosition;
 
    }

   
}
