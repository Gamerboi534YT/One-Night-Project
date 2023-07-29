using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] cameras; // Array to hold references to the cameras
    public float switchDelay = 1.0f; // Delay in seconds between camera switches
    public KeyCode customKey = KeyCode.C; // Custom keybind option
    public float cameraSensitivity = 50f; // Customizable cursor sensitivity for camera rotation

    public string officeCameraName = "Office Camera"; // The name of the camera that will be controlled

    private int currentCameraIndex; // Index of the current active camera
    private bool canSwitch = true; // Flag to control camera switching
    private float rotationAngle = 90f; // Current rotation angle of the camera (start at 90 degrees)

    void Start()
    {
        // Disable all cameras except the first one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // Set the first camera as the active one
        currentCameraIndex = 0;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }

    void Update()
    {
        // Check for camera switch input (use the customKey)
        if (Input.GetKeyDown(customKey) && canSwitch)
        {
            // Disable the current camera
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Move to the next camera index
            currentCameraIndex++;

            // If we reached the end of the camera array, go back to the first camera
            if (currentCameraIndex >= cameras.Length)
            {
                currentCameraIndex = 0;
            }

            // Enable the new current camera
            cameras[currentCameraIndex].gameObject.SetActive(true);

            // Set the rotation angle for the camera's local rotation if it's the "Office Camera"
            if (cameras[currentCameraIndex].name == officeCameraName)
            {
                cameras[currentCameraIndex].transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            }

            // Set the flag to prevent rapid switching
            canSwitch = false;

            // Start a coroutine to enable camera switching after the specified delay
            StartCoroutine(EnableSwitching());
        }

        // Check if the current camera is the "Office Camera" and apply rotation if it is
        if (cameras[currentCameraIndex].gameObject.activeSelf && cameras[currentCameraIndex].name == officeCameraName)
        {
            // Get the horizontal movement of the mouse
            float horizontalMovement = Input.GetAxis("Mouse X");

            // Update the rotation angle based on the mouse movement and sensitivity
            rotationAngle += horizontalMovement * cameraSensitivity * Time.deltaTime;

            // Limit the rotation angle to -180 and 180 degrees (total 360 degrees)
            rotationAngle = Mathf.Clamp(rotationAngle, 0f, 180f);

            // Set the rotation angle for the camera's local rotation
            cameras[currentCameraIndex].transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        }
    }

    // Coroutine to enable camera switching after a delay
    IEnumerator EnableSwitching()
    {
        yield return new WaitForSeconds(switchDelay);
        canSwitch = true;
    }
}
