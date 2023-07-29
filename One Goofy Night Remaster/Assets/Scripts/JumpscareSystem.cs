using UnityEngine;

public class AnimatronicAggression : MonoBehaviour
{
    public int aiNumber = 10;
    public int maxStages = 5;
    public float jumpscareDelay = 2f;
    public float minTeleportTime = 1f;
    public float maxTeleportTime = 200f;
    public GameObject mainGameObject;
    public Transform[] teleportLocations;

    private bool isJumpscaring = false;
    private int currentStage = 0;
    private bool canMoveToMainObject = false;

    private Animator animator;

    // Event delegate for stage 4 reached
    public delegate void Stage4Reached();
    public event Stage4Reached OnStage4Reached;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Start teleporting randomly between locations
        Invoke(nameof(TeleportRandomly), Random.Range(minTeleportTime, maxTeleportTime));
    }

    private int GetRandomNumber()
    {
        return Random.Range(1, 21);
    }

    private void ProgressStage()
    {
        if (currentStage >= maxStages)
        {
            Jumpscare();
        }
        else
        {
            currentStage++;
            Debug.Log("Animatronic has progressed to stage: " + currentStage);

            if (currentStage == maxStages - 1)
            {
                canMoveToMainObject = true;
                Debug.Log("Prepare for the final stage! The animatronic can now move to the main game object!");

                // Trigger the event when stage 4 is reached
                if (currentStage == 4)
                {
                    OnStage4Reached?.Invoke();
                }
            }
        }
    }

    private void Jumpscare()
    {
        Debug.Log("Jumpscare!");
        isJumpscaring = true;

        // Trigger the jumpscare animation or game over here

        Invoke(nameof(ResetAggression), jumpscareDelay);
    }

    private void ResetAggression()
    {
        isJumpscaring = false;
        currentStage = 0;
        canMoveToMainObject = false;
        Debug.Log("Animatronic aggression has been reset.");

        // Start teleporting again after aggression reset
        Invoke(nameof(TeleportRandomly), Random.Range(minTeleportTime, maxTeleportTime));
    }

    private void TeleportRandomly()
    {
        if (!isJumpscaring && teleportLocations.Length > 0)
        {
            // Choose a random teleport location from the list
            Transform randomTeleportLocation = teleportLocations[Random.Range(0, teleportLocations.Length)];

            // Set the robot's position to the chosen location
            transform.position = randomTeleportLocation.position;

            // Continue teleporting if the animatronic hasn't reached stage 5 yet
            if (currentStage < maxStages)
            {
                // Play the corresponding animation for the teleport location
                int teleportIndex = System.Array.IndexOf(teleportLocations, randomTeleportLocation);
                PlayTeleportAnimation(teleportIndex);

                Invoke(nameof(TeleportRandomly), Random.Range(minTeleportTime, maxTeleportTime));
            }
            else if (canMoveToMainObject)
            {
                // If the animatronic can move to the main game object, teleport to it instantly
                transform.position = mainGameObject.transform.position;

                // Play the corresponding animation for the main game object teleport
                PlayTeleportAnimation(teleportLocations.Length); // Use an index that's not present in the array

                // After teleporting to the main object, reset aggression and start teleporting again
                ResetAggression();
            }
        }
    }

    private void PlayTeleportAnimation(int index)
    {
        if (index >= 0 && index < teleportLocations.Length)
        {
            // Play the corresponding animation based on the teleport location index
            animator.SetInteger("TeleportIndex", index);
            animator.SetTrigger("Teleport");
        }
        else
        {
            // If the teleport index is not in the valid range, set the default animation
            animator.SetInteger("TeleportIndex", -1); // Use an index that's not present in the array
            animator.SetTrigger("Teleport");
        }
    }

    // Method to progress animatronic to stage 5
    public void ProgressToStage5()
    {
        if (currentStage == 4)
        {
            // If the animatronic is at stage 4 and the door was not closed in time,
            // instantly progress to stage 5
            Debug.Log("Stage 5 reached due to the door not being closed in time!");
            currentStage++;
            Jumpscare();
        }
    }

    private void Update()
    {
        if (!isJumpscaring && currentStage < maxStages)
        {
            int randomNumber = GetRandomNumber();
            if (randomNumber <= aiNumber)
            {
                ProgressStage();
            }
        }
    }
}
