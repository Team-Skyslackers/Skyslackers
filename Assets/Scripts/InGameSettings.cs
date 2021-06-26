using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettings : MonoBehaviour
{
    public Slider
        music_volume_slider,
        SFX_slider,
        music_delay_slider;
    public Text
        music_volume_display,
        SFX_display,
        music_delay_display;


    // Start is called before the first frame update
    void Start()
    {
        music_volume_slider.value = SettingsController.music_volume * 100;
        SFX_slider.value = SettingsController.SFX_volume * 100;
        music_delay_slider.value = SettingsController.music_delay;
    }

    // Update is called once per frame
    void Update()
    {
        music_volume_display.text = ((int)music_volume_slider.value).ToString();
        SFX_display.text = ((int)SFX_slider.value).ToString();
        music_delay_display.text = ((int)music_delay_slider.value).ToString();

        music_volume_slider.onValueChanged.AddListener((v) => SettingsController.music_volume = v / 100);
        SFX_slider.onValueChanged.AddListener((v) => SettingsController.SFX_volume = v/100);
        music_delay_slider.onValueChanged.AddListener((v) => SettingsController.music_delay = v);
    }
}
