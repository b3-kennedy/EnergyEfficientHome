using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrabText : MonoBehaviour
{
   public Slider originalSlider;
    public Slider secondSlider;
    void Start()
    {
        secondSlider.value = originalSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        secondSlider.value = originalSlider.value;
        
    }
}
