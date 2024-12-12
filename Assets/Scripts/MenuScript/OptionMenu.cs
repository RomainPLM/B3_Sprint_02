using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider volumeSlider = null;
    public Camera MainCamera = null;

    public void Start()
    {
        MainCamera = Camera.main;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void setVolume(float volume)
    {
        
        AudioListener.volume = volume;
    }

    public void applyVolume()
    {
        setVolume(volumeSlider.value);
    }
    
}
