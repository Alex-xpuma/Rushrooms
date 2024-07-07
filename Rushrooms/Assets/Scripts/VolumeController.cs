using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Устанавливаем начальное значение громкости из GameData
        volumeSlider.value = GameData.VolumeData.volume;

        // Подписываемся на изменение значения ползунка
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        audioSource.volume = volume;
        GameData.VolumeData.volume = volume;
    }
}
