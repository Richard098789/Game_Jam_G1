using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private bool playOnAwake = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        audioSource.loop = true;
        audioSource.clip = backgroundMusic;
        if (playOnAwake && backgroundMusic != null)
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        if (audioSource.clip != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void ChangeTrack(AudioClip newTrack)
    {
        if (audioSource.clip == newTrack) return; // Don't restart if already playing

        audioSource.Stop();
        audioSource.clip = newTrack;
        audioSource.Play();
    }
}
