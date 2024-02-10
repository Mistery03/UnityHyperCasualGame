using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class eyes : MonoBehaviour
{
    public float speed = 100f; // Duration of tween to move missile in
    public float waitTime = 2f; // Time to wait before super speed down
    public float superSpeedDuration = 0.5f; // Duration of super speed down
    public float superSpeedDistance = 10f; // Distance to move during super speed down
    public float rotationSpeed = 360f;
    public float unitDistance = 3f;

    public UnityEvent OnDestroy;

    private void Start()
    {
       

        Vector3 targetPosition = transform.position + Vector3.down * unitDistance;

        MoveObject(transform, targetPosition,1f);
    }

    private void MoveObject(Transform objectTransform, Vector3 targetPosition, float duration)
    {
        // Store the starting position
        Vector3 startingPosition = objectTransform.position;

        // Start a coroutine to smoothly move the object
        StartCoroutine(MoveObjectCoroutine(objectTransform, startingPosition, targetPosition, duration));
    }

    private IEnumerator MoveObjectCoroutine(Transform objectTransform, Vector3 startingPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the interpolation value (0 to 1)
            float t = elapsedTime / duration;

            // Use Vector3.Lerp to interpolate between the starting and target positions
            objectTransform.position = Vector3.Lerp(startingPosition, targetPosition, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the end of the frame
            yield return null;
        }

        // Ensure the object reaches the target position exactly
        objectTransform.position = targetPosition;

        // Rotate the object to indicate readiness to go down
        StartCoroutine(RotateObject(objectTransform));

        // Wait before initiating the super speed down
        yield return new WaitForSeconds(waitTime);

        

        // Move the object downward with super speed
        Vector3 endPosition = targetPosition - Vector3.up * superSpeedDistance;
        StartCoroutine(MoveObjectDown(objectTransform, endPosition, superSpeedDuration));
    }
    private IEnumerator RotateObject(Transform objectTransform)
    {
        while (true)
        {
            // Rotate the object continuously
            objectTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveObjectDown(Transform objectTransform, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = objectTransform.position;

        // Move the object downward with super speed
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            objectTransform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectTransform.position = targetPosition;

        // Object reached the target position, perform actions here
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "missile")
        {
            Destroy(collision.gameObject);
            OnDestroy.Invoke();
            Destroy(gameObject);
        }
    }


}
