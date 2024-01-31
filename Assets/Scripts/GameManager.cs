using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public Text scoreText, scoreHudText;
    public float scoreAmount;
    public float scoreMultiplier;
    public float totalScore;
    public float speedMultiplier ;

    public meteor meteorite;

    float _spaceshipFuelPercentage;

    private void Awake()
    {
        scoreAmount = 100;
        scoreMultiplier = 1;
        totalScore = 0;
        speedMultiplier = 1;
    }

    private void Update()
    {
        CalculateScore();
        meteorite.SpeedMultiplier = getSpeedMultiplier();
    }

    public void scoreHUD(float currentScore)
    {
        scoreHudText.text = "SCORE: "+((int)currentScore).ToString();
    }

    public void GameOver(float totalScore)
    {
        gameOverUI.SetActive(true);
        scoreText.text = "SCORE: " + ((int)totalScore).ToString();
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
        totalScore += scoreMultiplier * scoreAmount * Time.fixedUnscaledDeltaTime;
    }
}
