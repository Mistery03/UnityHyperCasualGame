using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnLowSpeed, OnLowFuel;
    public GameObject gameOverUI;
    public Text ScoreText, ScoreHudText, LevelText, LevelHudText, WarningFuelAlertText, WarningSpeedAlertText;
    public RectTransform phoneSymbolTransform;
    public float scoreAmount;
    public float scoreMultiplier, speedMultiplier;
    public float totalScore;
    public int currentLevel;
    public float currentSpeed, originalSpeed;
    private Vector3 targetPosition;
    public float smoothness = 0.5f;

    bool isFuelWarningDisplayed, isSpeedWarningDisplayed;

    public meteor meteorite;

    float _spaceshipFuelPercentage, _spaceShipDamage;

    private void Awake()
    {
        scoreAmount = 100;
        scoreMultiplier = 1;
        totalScore = 0;
        currentLevel = 0;
        speedMultiplier = 1;
        levelHUD(currentLevel);
        scoreHUD(totalScore);
        

    }

    private void Start()
    {
        WarningFuelAlertText.text = null;
        WarningSpeedAlertText.text = null;
        targetPosition = phoneSymbolTransform.position + Vector3.down * 10;
    }

    private void Update()
    {
        CalculateScore();
        scoreHUD(totalScore);
        //meteorite.SpeedMultiplier = getSpeedMultiplier();
        phoneSymbolTransform.position = Vector3.Lerp(phoneSymbolTransform.position, targetPosition, smoothness * Time.deltaTime);


        if (currentSpeed <= 0)
            OnLowSpeed.Invoke();

        if (currentSpeed <= 2 * speedMultiplier && !isSpeedWarningDisplayed)
        {
            WarningSpeedAlertText.text = "LOW SPEED!";
            isSpeedWarningDisplayed = true;
            StartSpeedBlink();
        }else if (currentSpeed > 2 * speedMultiplier && isSpeedWarningDisplayed)
        {
            WarningSpeedAlertText.text = "";
            isSpeedWarningDisplayed = false;
            StopSpeedBlink();
        }


        if (getSpaceshipFuelLevel() <= 0.30 && !isFuelWarningDisplayed)
        {
            WarningFuelAlertText.text = "LOW FUEL!";
            isFuelWarningDisplayed = true;
            StartFuelBlink();
        }
        else if(getSpaceshipFuelLevel() > 0.30 && isFuelWarningDisplayed)
        {
            WarningFuelAlertText.text = "";
            isFuelWarningDisplayed = false;
            StopFuelBlink();
        }
           
           



    }

    public void scoreHUD(float currentScore)
    {
        ScoreHudText.text = "SCORE: "+((int)currentScore).ToString();
    }

    public void levelHUD(int currentLevel)
    {
        LevelHudText.text = "LEVEL: " + (currentLevel).ToString();
    }

    public void GameOver(float totalScore)
    {
        gameOverUI.SetActive(true);
        ScoreText.text = "SCORE: " + ((int)totalScore).ToString();
        LevelText.text = "LEVEL: " + (currentLevel).ToString();
    }

    public void restart()
    {
        SceneManager.LoadScene("GameScene");

    }

    public void returnMainMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void setSpaceshipFuelLevel(float fuelPercentage)
    {
        _spaceshipFuelPercentage = fuelPercentage;
    }

    public float getSpaceshipFuelLevel()
    {
        return _spaceshipFuelPercentage;
    }
    public void setSpaceshipDamage(float dmg)
    {
       _spaceShipDamage = dmg;
    }
    public float getSpaceshipDamage()
    {
        return _spaceShipDamage;
    }

    public void setScoreMultiplier(float scoreMultiplier)
    {
        this.scoreMultiplier = scoreMultiplier;
    }

    public float getScoreMultiplier()
    {
        return scoreMultiplier;
    }

    public void setSpeedMultiplier(float speedMultiplier)
    {
        this.speedMultiplier = speedMultiplier;
    }

    public float getSpeedMultiplier()
    {
        return speedMultiplier;
    }

    private void CalculateScore()
    {
        totalScore += scoreMultiplier *  currentSpeed * scoreAmount * Time.deltaTime;
      
    }

    public void unPause()
    {
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int score)
    {
        totalScore += score;
      
        
    }

    public void disableHUDTexts()
    {
        LevelHudText.enabled = false;
        ScoreHudText.enabled = false;
        WarningSpeedAlertText.enabled = false;
        WarningFuelAlertText.enabled= false;

    }

    public void enableHUDTexts()
    {
        LevelHudText.enabled = true;
        ScoreHudText.enabled = true;
        WarningSpeedAlertText.enabled = true;
        WarningFuelAlertText.enabled = true;
    }

    public void setCurrentSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public void setOriginalSpeed(float speed)
    {
        originalSpeed = speed;
    }

    private IEnumerator FuelBlink()
    {
        while (true)
        {
            switch (WarningFuelAlertText.color.a.ToString())
            {
                case "0":
                    WarningFuelAlertText.color = new Color(WarningFuelAlertText.color.r, WarningFuelAlertText.color.g, WarningFuelAlertText.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    WarningFuelAlertText.color = new Color(WarningFuelAlertText.color.r, WarningFuelAlertText.color.g, WarningFuelAlertText.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }

          
        }
    }

    private IEnumerator SpeedBlink()
    {
        while (true)
        {
            switch (WarningSpeedAlertText.color.a.ToString())
            {
                case "0":
                    WarningSpeedAlertText.color = new Color(WarningSpeedAlertText.color.r, WarningSpeedAlertText.color.g, WarningSpeedAlertText.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    WarningSpeedAlertText.color = new Color(WarningSpeedAlertText.color.r, WarningSpeedAlertText.color.g, WarningSpeedAlertText.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }


        }
    }

    


    public void StartFuelBlink()
    {
        StopCoroutine("FuelBlink");
        StartCoroutine("FuelBlink");
    }

    public void StopFuelBlink()
    {
        StopCoroutine("FuelBlink");

    }
    public void StartSpeedBlink()
    {
        StopCoroutine("SpeedBlink");
        StartCoroutine("SpeedBlink");
    }

    public void StopSpeedBlink()
    {
        StopCoroutine("SpeedBlink");
   
    }







}
