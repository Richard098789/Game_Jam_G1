using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance { get; private set; }

    [Header("Sound Library")]
    public AudioClip clubHittingBallSound;
    public AudioClip ballHittingWoodSound;
    public AudioClip ballHittingMetalSound;
    public AudioClip winSound;
    public AudioClip windBlowSound;
    public AudioClip whooshSound;
    public AudioClip dragSound;

    [Header("Pool Settings")]
    [SerializeField] private int poolSize = 10;
    private AudioSource[] sourcePool;
    private int currentPoolIndex = 0;

    // TRACKING FOR DRAG SOUND
    private AudioSource dragSource; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetupAudioSourcePool();
    }

    private void SetupAudioSourcePool()
    {
        sourcePool = new AudioSource[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.playOnAwake = false;
            newSource.loop = false;
            newSource.spatialBlend = 0f; 

            sourcePool[i] = newSource;
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (clip == null) return;

        AudioSource source = sourcePool[currentPoolIndex];

        // Ensure looping is turned off for standard, one-shot SFX
        source.loop = false; 
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

        currentPoolIndex = (currentPoolIndex + 1) % poolSize;
    }

    // NEW: Plays the drag sound looping, only if it isn't already playing
    public void PlayDragSound(float volume = 1f, float pitch = 1f)
    {
        if (dragSound == null) return;
        
        // If it's already playing, do nothing and return out early
        if (dragSource != null && dragSource.isPlaying) return;

        // Grab the next available source in the pool
        dragSource = sourcePool[currentPoolIndex];
        
        dragSource.clip = dragSound;
        dragSource.volume = volume;
        dragSource.pitch = pitch;
        dragSource.loop = true; // Make it loop seamlessly while dragging
        dragSource.Play();

        currentPoolIndex = (currentPoolIndex + 1) % poolSize;
    }

    // NEW: Safely stops the drag sound loop
    public void StopDragSound()
    {
        if (dragSource != null && dragSource.isPlaying && dragSource.clip == dragSound)
        {
            dragSource.Stop();
            dragSource.loop = false; // Reset the loop flag for pool recycling
        }
    }
}
