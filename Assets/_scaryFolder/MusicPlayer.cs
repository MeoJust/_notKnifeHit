using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    AudioSource _audioSource;

    public AudioClip WoodSFX;
    public AudioClip KnifeSFX;
    public AudioClip MelonSFX;
    public AudioClip WinSFX;
    public AudioClip LooseSFX;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip, 1f);
    }

}
