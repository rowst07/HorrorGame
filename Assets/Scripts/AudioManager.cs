using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource ambienceSource;
    public AudioSource sfxSource;
    public AudioSource chaseSource;

    [Header("Ambience")]
    public AudioClip villageAmbience;
    public AudioClip graveyardAmbience;
    public AudioClip bunkerAmbience;
    public AudioClip menuAmbience;

    [Header("Zombie")]
    public AudioClip zombieChase;
    public AudioClip zombieRoar;

    [Header("Interactions")]
    public AudioClip keyPickup;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayAmbience(villageAmbience);
    }

    public void PlayAmbience(AudioClip clip)
    {
        ambienceSource.clip = clip;
        ambienceSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StartChase()
    {
        chaseSource.clip = zombieChase;
        chaseSource.Play();
    }

    public void StopChase()
    {
        chaseSource.Stop();
    }

    public void PlayZombieRoar()
    {
        sfxSource.PlayOneShot(zombieRoar);
    }
}