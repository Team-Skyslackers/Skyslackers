using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource source;
    public GameObject explosionEffectGenerator;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayExplosionSound(Vector3 beamPosition)
    {
        source.volume = SettingsController.SFX_volume;
        source.Play();
        Instantiate(explosionEffectGenerator, beamPosition, gameObject.transform.rotation);
    }
}
