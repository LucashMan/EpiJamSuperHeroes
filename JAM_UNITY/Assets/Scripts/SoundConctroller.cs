using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundConctroller : MonoBehaviour
{
    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void OnSliderVolumeChanged()
    {
        AudioListener.volume = slider.value;
    }
    

}
