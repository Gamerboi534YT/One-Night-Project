using UnityEngine;
using UnityEngine.UI;

public class HideGUIItems : MonoBehaviour
{
    // Reference to the Canvas or the parent GameObject that contains the GUI items
    public GameObject guiCanvas;

    // Reference to the GUI Button that will be used to trigger the hiding/showing
    public Button toggleButton;

    // Boolean flag to keep track of the visibility state of GUI items
    private bool guiVisible = false; // Start with the Canvas invisible

    // Audio clips for the opening and closing sounds
    public AudioClip openingSound;
    public AudioClip closingSound;

    private AudioSource audioSource;

    private void Start()
    {
        // Set the Canvas (or parent GameObject) to inactive (invisible) at the start
        guiCanvas.SetActive(false);

        // Add a listener to the button's onClick event
        toggleButton.onClick.AddListener(ToggleGUIItems);

        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void ToggleGUIItems()
    {
        // Flip the visibility state
        guiVisible = !guiVisible;

        // Play the corresponding sound based on the visibility flag
        if (guiVisible)
        {
            // Show GUI items and play the opening sound
            guiCanvas.SetActive(true);
            if (openingSound != null)
                audioSource.PlayOneShot(openingSound);
        }
        else
        {
            // Hide GUI items and play the closing sound
            guiCanvas.SetActive(false);
            if (closingSound != null)
                audioSource.PlayOneShot(closingSound);
        }

        // If you want to hide individual GUI elements (e.g., Image, Text, etc.), 
        // you can use this code instead:
        // foreach (Transform child in guiCanvas.transform)
        // {
        //     child.gameObject.SetActive(guiVisible);
        // }
    }
}
