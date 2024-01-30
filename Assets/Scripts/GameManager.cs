using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public Text scoreText, scoreHudText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
