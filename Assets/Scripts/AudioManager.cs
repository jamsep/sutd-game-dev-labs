using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource[] allAudioSources;

    void Awake()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    public void PauseAll()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in allAudioSources)
        {
            if (source.isPlaying)
                source.Pause();
        }
    }

    public void ResumeAll()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in allAudioSources)
        {
            if (source != null)
                source.UnPause();
        }
    }
}
