using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MeteorSpawnInfo
{
    public GameObject meteorPrefab; // Reference to the meteor prefab
    [Range(0, 100)] public float spawnProbability; // Probability of spawning this meteor (in percentage)
}

public class MeteorSpawnerAlt : MonoBehaviour
{
    [SerializeField] private float spawnRate = 3f; // Base spawn rate
    private float timer; // Timer to keep track of spawn time
    public UnityEvent OnMeteorDestroy; // Event triggered when a meteor is destroyed
    public GameManager GameManager;
    public MeteorSpawnInfo[] meteorSpawnInfo; // Array of meteor spawn information

    void Start()
    {
        // Start the timer immediately
        timer = spawnRate;
    }

    void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // If the timer reaches zero or below, spawn a new meteor
        if (timer <= 0f)
        {
            SpawnMeteor();
            // Reset the timer to the spawn rate
            timer = spawnRate;
        }
    }

    void SpawnMeteor()
    {
        // Calculate the total probability sum
        float totalProbabilitySum = 0f;
        foreach (var info in meteorSpawnInfo)
        {
            totalProbabilitySum += info.spawnProbability;
        }

        // Generate a random number within the total probability sum range
        float randomNumber = Random.Range(0f, totalProbabilitySum);

        // Loop through the meteor spawn info array and determine which meteor to spawn based on the random number
        float cumulativeProbabilitySum = 0f;
        foreach (var info in meteorSpawnInfo)
        {
            cumulativeProbabilitySum += info.spawnProbability;
            if (randomNumber <= cumulativeProbabilitySum)
            {
                // Instantiate the selected meteor prefab at the spawner's position
                GameObject newMeteor = Instantiate(info.meteorPrefab, transform.position, Quaternion.identity);
                // Set the speed multiplier of the spawned meteor
                newMeteor.GetComponent<meteor>().SpeedMultiplier = GameManager.getSpeedMultiplier();
                // Subscribe to the OnDestroy event of the meteor object
                newMeteor.GetComponent<meteor>().OnDestroy.AddListener(InvokeMeteorDestroyEvent);
                // Destroy the meteor after a certain time
                Destroy(newMeteor, 10f);
                return; // Exit the loop once a meteor is spawned
            }
        }
    }

    // Method to invoke the OnMeteorDestroy event
    void InvokeMeteorDestroyEvent()
    {
        OnMeteorDestroy.Invoke();
    }
}
