using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class MoveBackGround : MonoBehaviour
{
    public float OriginalSpeed;
    public float currentSpeed;

    public GameManager GameManager;

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
        float fuelLevel = GameManager.getSpaceshipFuelLevel();
        float targetSpeed;

        if (fuelLevel > 0.45 && fuelLevel <= 0.65)
        {
            GameManager.setSpeedMultiplier(2f);
            GameManager.setScoreMultiplier(0.1f);
         
        }
        else if(fuelLevel > 0.15 && fuelLevel <= 0.45)
        {
            GameManager.setSpeedMultiplier(4f);
            GameManager.setScoreMultiplier(1f);
   
        } 
        else if (fuelLevel <= 0.15)
        {
            GameManager.setSpeedMultiplier(16f);
            GameManager.setScoreMultiplier(10f);
  
        }
        else
        {
            GameManager.setSpeedMultiplier(1f);
            GameManager.setScoreMultiplier(0.01f);
      
        }
      

        float newPosition = Mathf.Repeat(Time.time * currentSpeed * GameManager.getSpeedMultiplier(), _width);


        transform.position = _startPos + Vector2.down * newPosition;
 
    }

   
}
