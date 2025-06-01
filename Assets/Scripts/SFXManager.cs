using UnityEngine;

public enum SFXType { reload, shoot, enemyDie, hit, warning };

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource sfxSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnValidate()
    {
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();

        }
    }

    public void PlaySFX(SFXType type)
    {
        var clip = Resources.Load<AudioClip>($"SFXs/{type}");
        sfxSource.PlayOneShot(clip);
    }
}
