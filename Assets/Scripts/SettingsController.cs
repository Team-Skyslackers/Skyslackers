using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static float SFX_volume = 0.3f, bolt_speed = 2;  // bolt speed = unit distance travelled per frame
    public Slider SFX_slider, bolt_speed_slider;
    public Text SFX_display, bolt_speed_display;

    private void Start()
    {
        SFX_slider.value = SFX_volume;
        bolt_speed_slider.value = bolt_speed;
        SFX_slider.onValueChanged.AddListener((v) =>
        {
            SFX_volume = v;
        });
        bolt_speed_slider.onValueChanged.AddListener((v) =>
        {
            bolt_speed = v;
        });
    }

    private void Update()
    {
        SFX_display.text = SFX_volume.ToString("N");
        bolt_speed_display.text = bolt_speed.ToString("N");
    }
}
