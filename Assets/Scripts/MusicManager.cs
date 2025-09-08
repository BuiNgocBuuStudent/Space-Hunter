
using UnityEngine;

public enum MusicType {  combatTheme1, combatTheme2, menuTheme };

public class MusicManager : MonoBehaviour 
{
    private static MusicManager _instance;
    public static MusicManager Instance => _instance;

    public AudioSource musicSource;
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
