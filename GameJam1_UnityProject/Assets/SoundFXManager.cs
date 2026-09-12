using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance { get; private set; }

    [Header("Sound Library")]
    public AudioClip clubHittingBallSound;
    public AudioClip ballHittingWoodSound;
    public AudioClip ballHittingMetalSound;
    public AudioClip winSound;
    public AudioClip windBlowSound;

    [Header("Pool Settings")]
    [SerializeField] private int poolSize = 10;
    private AudioSource[] sourcePool;
    private int currentPoolIndex = 0;

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
            
            // Set to 2D sound by default (0 = fully 2D, 1 = fully 3D)
            newSource.spatialBlend = 0f; 

            sourcePool[i] = newSource;
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        if (clip == null) return;

        AudioSource source = sourcePool[currentPoolIndex];

        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

        currentPoolIndex = (currentPoolIndex + 1) % poolSize;
    }

    
}
