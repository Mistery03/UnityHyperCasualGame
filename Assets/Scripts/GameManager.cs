using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnLowSpeed, OnLowFuel;
    public GameObject gameOverUI;
    public Text ScoreText, ScoreHudText, LevelText, LevelHudText, WarningAlertText;
    public float scoreAmount;
    public float scoreMultiplier, speedMultiplier;
    public float totalScore;
    public int currentLevel;
    public float currentSpeed;

    bool isWarningDisplayed;

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
        WarningAlertText.text = null;
    }

    private void Update()
    {
        CalculateScore();
        scoreHUD(totalScore);
        meteorite.SpeedMultiplier = getSpeedMultiplier();

        if(currentSpeed <= 0)
            OnLowSpeed.Invoke();


        if(getSpaceshipFuelLevel() <= 0.30 && !isWarningDisplayed)
        {
            WarningAlertText.text = "LOW FUEL!";
            isWarningDisplayed = true;
            StartBlink();
        }
        else if(getSpaceshipFuelLevel() > 0.30 && isWarningDisplayed)
        {
            WarningAlertText.text = "";
            isWarningDisplayed = false;
            StopBlink();
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
        totalScore += scoreMultiplier * scoreAmount * Time.deltaTime;
      
    }

    public void unPause()
    {
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore()
    {
        totalScore += 1000;
      
        
    }

    public void disableHUDTexts()
    {
        LevelHudText.enabled = false;
        ScoreHudText.enabled = false;
        WarningAlertText.enabled= false;

    }

    public void enableHUDTexts()
    {
        LevelHudText.enabled = true;
        ScoreHudText.enabled = true;
        WarningAlertText.enabled = true;
    }

    public void setCurrentSpeed(float speed)
    {
        currentSpeed = speed;
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            switch (WarningAlertText.color.a.ToString())
            {
                case "0":
                    WarningAlertText.color = new Color(WarningAlertText.color.r, WarningAlertText.color.g, WarningAlertText.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    WarningAlertText.color = new Color(WarningAlertText.color.r, WarningAlertText.color.g, WarningAlertText.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    public void StartBlink()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }

    public void StopBlink()
    {
        StopCoroutine("Blink");
   
    }







}
