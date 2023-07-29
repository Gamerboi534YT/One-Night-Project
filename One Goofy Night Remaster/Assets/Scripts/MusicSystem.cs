using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // Reference to the background music audio clip
    public bool playOnStart = true; // Whether to play the music automatically when the scene starts
    public float volume = 0.5f; // The volume of the background music (0.0f to 1.0f)

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // Load the background music clip
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
        }

        // Set the volume
        audioSource.volume = volume;

        // Enable looping for the background music
        audioSource.loop = true;

        // Play the background music on start if enabled
        if (playOnStart)
        {
            PlayBackgroundMusic();
        }
    }

    public void PlayBackgroundMusic()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    public void PauseBackgroundMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    public void StopBackgroundMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
