using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Slider volumeSlider = null;
    public Camera MainCamera = null;

    public AudioClip[] audios;

    public void Start()
    {
        MainCamera = Camera.main;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

        Screen.fullScreen = isFullScreen;
    }

    public void setVolume(float volume)
    {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

        AudioListener.volume = volume;
    }

    public void applyVolume()
    {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

        setVolume(volumeSlider.value);
    }
    
}
