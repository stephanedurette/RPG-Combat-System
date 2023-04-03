using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private RectTransform panelContent;
    [SerializeField] private Slider musicVolumeSlider, soundVolumeSlider;

    public void ChangeSoundVolume()
    {
        Singleton.Instance.SoundManager.Volume = soundVolumeSlider.value;
    }

    public void ChangeMusicVolume()
    {
        Singleton.Instance.MusicManager.Volume = musicVolumeSlider.value;
    }

    public void TogglePanel(bool open)
    {
        if (open)
        {
            musicVolumeSlider.value = Singleton.Instance.MusicManager.Volume;
            soundVolumeSlider.value = Singleton.Instance.SoundManager.Volume;
        }

        Time.timeScale = open ? 0f : 1f;

        panelContent.gameObject.SetActive(open);
    }
}
