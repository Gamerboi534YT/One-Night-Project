using UnityEngine;
using System.Collections;

public class CharacterTeleport : MonoBehaviour
{
    public Transform[] teleportPoints; // Array of positions where the character can teleport
    public bool randomTeleportInterval = false; // Toggle random teleport interval on/off
    public float minTeleportInterval = 1f; // Minimum teleport interval in seconds
    public float maxTeleportInterval = 200f; // Maximum teleport interval in seconds

    private int currentTeleportIndex = 0;
    private Transform characterTransform;
    private bool isTeleporting = false;

    void Start()
    {
        characterTransform = transform;
        if (teleportPoints.Length == 0)
        {
            Debug.LogWarning("No teleport points assigned to the CharacterTeleport script on " + gameObject.name);
        }
        StartCoroutine(TeleportCoroutine());
    }

    void Update()
    {
        // You can add any other character-related logic here if needed
    }

    IEnumerator TeleportCoroutine()
    {
        while (true) // Infinite loop for continuous teleporting
        {
            if (!isTeleporting && teleportPoints.Length > 0)
            {
                // Start teleporting coroutine
                StartCoroutine(DoTeleport());
            }

            float interval = randomTeleportInterval ? Random.Range(minTeleportInterval, maxTeleportInterval) : maxTeleportInterval;

            // Wait for the specified teleport interval before attempting the next teleport
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator DoTeleport()
    {
        isTeleporting = true;

        // Teleport the character to the next teleport point
        characterTransform.position = teleportPoints[currentTeleportIndex].position;

        // Move to the next teleport point in the array
        currentTeleportIndex++;
        if (currentTeleportIndex >= teleportPoints.Length)
        {
            currentTeleportIndex = 0; // Wrap around to the first teleport point if we reached the end
        }

        // Wait for a short duration to simulate the teleportation effect
        yield return new WaitForSeconds(0.1f);

        isTeleporting = false;
    }
}
