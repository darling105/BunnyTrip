using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource fxAudioSource;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip pickupCarrotSFX;
    [SerializeField] private AudioClip winningSFX;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 60;
        // Phát nhạc nền khi game khởi động
        if (bgAudioSource != null && backgroundMusic != null)
        {
            bgAudioSource.clip = backgroundMusic;
            bgAudioSource.loop = true;
            bgAudioSource.Play();
        }
    }

    public void PlayPickupCarrot()
    {
        fxAudioSource.PlayOneShot(pickupCarrotSFX);
    }

    public void PlayWinning()
    {
        fxAudioSource.PlayOneShot(winningSFX);
    }

}
