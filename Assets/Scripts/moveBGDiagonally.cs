using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBGDiagonally : MonoBehaviour
{
    public float speed = 2f;
    public Transform bottomLeftBackground; // Reference to the bottom-left background GameObject
    public Transform bottomRightBackground; // Reference to the bottom-right background GameObject
    public Camera mainCamera; // Reference to the main camera

    private void Awake()
    {
        Time.timeScale = 1.0f;
        // Move bottom-right background diagonally
        bottomRightBackground.Translate(Vector3.up * speed * Time.deltaTime);
        bottomRightBackground.Translate(Vector3.left * speed * Time.deltaTime);
        CheckBackgroundPosition(bottomRightBackground);
    }

    void Update()
    {
 
       

        // Move bottom-right background diagonally
        bottomRightBackground.Translate(Vector3.up * speed * Time.deltaTime);
        bottomRightBackground.Translate(Vector3.left * speed * Time.deltaTime);


        CheckBackgroundPosition(bottomRightBackground);
    }

    // Helper method to check and reset background position if it moves out of the screen
    void CheckBackgroundPosition(Transform background)
    {
        // Convert background position to viewport space (ranges from 0 to 1)
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(background.position);

        float resetDistance = 1f; // Adjust as needed for smoother repeat

        // Check if the background is outside the camera's viewport
        if (viewportPos.x < -resetDistance || viewportPos.x > 1 + resetDistance || viewportPos.y < -resetDistance || viewportPos.y > 1 + resetDistance)
        {
            // Reset background position to the opposite corner of the camera's view
            background.position = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(mainCamera.transform.position.z - background.position.z)));
        }
    }
}
