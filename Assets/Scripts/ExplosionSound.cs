using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayExplosionSound()
    {
        source.volume = SettingsController.SFX_volume;
        source.Play();
    }
}
