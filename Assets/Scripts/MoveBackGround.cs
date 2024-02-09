using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
        currentSpeed = OriginalSpeed * Time.fixedDeltaTime;
        

    }

    // Update is called once per frame
    void Update()
    {
        float fuelLevel = GameManager.getSpaceshipFuelLevel();


        if(fuelLevel >= 0.65)
        {
            GameManager.setSpeedMultiplier(1f);
            GameManager.setScoreMultiplier(0.01f);

        } 
        
        if (fuelLevel >= 0.45f && fuelLevel < 0.65f)
        {
            GameManager.setSpeedMultiplier(2f);
            GameManager.setScoreMultiplier(0.1f);
         
        }
         
        if(fuelLevel >= 0.15f && fuelLevel < 0.45f)
        {
            
            GameManager.setSpeedMultiplier(4f);
            GameManager.setScoreMultiplier(1f);
   
        } 
         
        if (fuelLevel < 0.15f)
        {
           
            GameManager.WarningAlertText.text = "LOW FUEL!!!";
            //GameManager.StartBlink();
            GameManager.setSpeedMultiplier(8f);
            GameManager.setScoreMultiplier(10f);
  
        }
        
      

        float newPosition = Mathf.Repeat(Time.time * currentSpeed * GameManager.getSpeedMultiplier(), _width);

        transform.position = _startPos + Vector2.down * newPosition;

       
        GameManager.setCurrentSpeed(currentSpeed);
 
    }

    public void increaseSpeed()
    {
        currentSpeed += 2;
    }

    public void decreaseSpeed()
    {
        currentSpeed -= 2;
    }

    public float getCurrentSpeed() { return  currentSpeed; }


}
