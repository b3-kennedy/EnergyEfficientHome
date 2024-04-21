using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public Toggle musicToggle;

    public AudioSource musicSource;
    public AudioClip[] musicClips;

    public AudioSource washingMachineAudio;

    private int currentClipIndex = 0;

    public bool isMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This ensures that this object persists across scene changes
        }
        else
        {
            Destroy(gameObject); // If another AudioManager already exists, destroy this one
        }
    }

    private void OnEnable()
    {
        musicToggle.onValueChanged.AddListener(ToggleMusic);
    }
    public void ToggleMusic(bool play)
    {
        musicSource.mute = !play;
        isMuted = !play;
    }
    public void PlayBackgroundMusic()
    {
        musicSource.clip = musicClips[currentClipIndex];
        musicSource.Play();
    }

    public void ChangeMusic(int index)
    {
        if (index >= 0 && index < musicClips.Length)
        {
            currentClipIndex = index;
            PlayBackgroundMusic();
            Debug.Log("change music to index " + currentClipIndex + " now.");
        }
    }
    public void ToggleMute()
    {
        isMuted = !isMuted;
        musicSource.mute = isMuted;
    }

    public void PlayWahingMachineAudio()
    {
        //washingMachineAudio.volume -= 12;
        washingMachineAudio.Play();
    }

}
