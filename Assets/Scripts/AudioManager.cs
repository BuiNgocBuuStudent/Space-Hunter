using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("-------- Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------- Audio Clip ------")]
    public AudioClip theme;
    public AudioClip[] combatSFX;
    public AudioClip reload;
    public AudioClip shoot;
    public AudioClip enemyDie;
    public AudioClip hit;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int index = Random.Range(0, combatSFX.Length);
        PlaySound(combatSFX[index]);
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}
