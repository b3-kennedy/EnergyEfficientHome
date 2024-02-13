using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip radiatorBreakSound;
    public AudioClip windowSmash;
    public AudioClip phoneNotification;
    public AudioClip winTaskSound;
    public AudioClip incorrectAnswer;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
