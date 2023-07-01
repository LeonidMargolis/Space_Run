using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource collectSound;
    private AudioSource mainThemeSound;
    private AudioSource hitSound;

    public static SoundManager Instance;

    private void Awake()
    {
        if (SoundManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        SoundManager.Instance = this;
    }
    private void OnDestroy()
    {
        SoundManager.Instance = null;
    }

    private void Start()
    {
        collectSound = transform.Find("Sound_CollectSound").GetComponent<AudioSource>();
        mainThemeSound = transform.Find("Sound_MainTheme").GetComponent <AudioSource>();
        hitSound = transform.Find("Sound_HitSound").GetComponent<AudioSource>();
    }

    public void PlayCollectSound()
    {
        collectSound.Play();
    }

    public void PlayMainThemeSound()
    {
        mainThemeSound.Play();
    }
    public void PlayHitSound()
    {
        hitSound.Play();
    }
    public void StopMainThemeSound()
    {
        mainThemeSound.Stop();
    }
}
