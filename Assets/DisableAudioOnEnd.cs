using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAudioOnEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool ended = false;
    // Update is called once per frame
    void Update()
    {
        if (TimeManager.Instance.gameEnded && !ended)
        {
            gameObject.GetComponent<AudioManager>().enabled = false;
            ended = true;
        }
        
    }
}
