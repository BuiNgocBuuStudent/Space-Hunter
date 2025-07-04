using UnityEngine;

public enum SFXType { reload, shoot, enemyDie, hit, warning };

public class SFXManager : MonoBehaviour
{
    private static SFXManager _instance;
    public static SFXManager Instance => _instance;

    public AudioSource sfxSource;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
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
