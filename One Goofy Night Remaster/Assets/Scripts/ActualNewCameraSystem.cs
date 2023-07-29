using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    // The customizable switch button
    public Button switchButton;

    // The customizable switch to main button
    public Button switchToMainButton;

    // Reference to the main camera
    public Camera mainCamera;

    // List of cameras to switch between (excluding the main camera)
    public Camera[] cameras;

    // Reference to the starting camera GameObject
    public GameObject startingCamera;

    // Index to keep track of the currently active camera
    private int activeCameraIndex = 0;

    // Audio clip to play when switching between cameras
    public AudioClip switchSoundClip;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Flag to check if the switch sound has been played
    private bool hasPlayedSwitchSound = false;

    private void Start()
    {
        // Set the initial camera as active based on the startingCamera GameObject reference
        if (startingCamera != null)
        {
            activeCameraIndex = GetCameraIndex(startingCamera);
            ActivateCamera(activeCameraIndex);
        }

        // Attach the OnSwitchButtonClicked method to the switch button's click event
        switchButton.onClick.AddListener(OnSwitchButtonClicked);

        // Attach the OnSwitchToMainButtonClicked method to the switch to main button's click event
        switchToMainButton.onClick.AddListener(OnSwitchToMainButtonClicked);

        // Get or add the AudioSource component to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private int GetCameraIndex(GameObject cameraObj)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].gameObject == cameraObj)
            {
                return i;
            }
        }
        return 0; // If the camera is not found, return the main camera index as a fallback.
    }

    private void ActivateCamera(int index)
    {
        // Deactivate all cameras (excluding the main camera)
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // Activate the selected camera
        cameras[index].gameObject.SetActive(true);

        // Update the active camera index
        activeCameraIndex = index;

        // Reset the switch sound flag
        hasPlayedSwitchSound = false;
    }

    private void OnSwitchButtonClicked()
    {
        // Move to the next camera (cycling back to the first if necessary)
        activeCameraIndex = (activeCameraIndex + 1) % cameras.Length;

        // Activate the new camera
        ActivateCamera(activeCameraIndex);

        // Play the switch sound if it hasn't been played yet
        if (!hasPlayedSwitchSound && switchSoundClip != null)
        {
            audioSource.PlayOneShot(switchSoundClip);
            hasPlayedSwitchSound = true;
        }
    }

    private void OnSwitchToMainButtonClicked()
    {
        // Deactivate the current active camera
        cameras[activeCameraIndex].gameObject.SetActive(false);

        // Activate the main camera
        mainCamera.gameObject.SetActive(true);

        // Reset the active camera index to 0 (main camera)
        activeCameraIndex = 0;

        // Reset the switch sound flag
        hasPlayedSwitchSound = false;
    }
}
