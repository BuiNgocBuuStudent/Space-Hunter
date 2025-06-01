
using UnityEngine;

public enum MusicType {  combatTheme1, combatTheme2, menuTheme };

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource musicSource;
    private void Awake()
    {
        if(Instance == null)
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
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();

        }
    }
    public void PlayMusic(MusicType type)
    {
        var clip = Resources.Load<AudioClip>($"Musics/{type}");
        musicSource.clip = clip;
        musicSource.Play();
    }
}
