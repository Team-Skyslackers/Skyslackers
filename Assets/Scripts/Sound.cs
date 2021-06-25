using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Beam(Clone)"){
        // Debug.Log("Sound played");
        source.volume = SettingsController.SFX_volume;
        source.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
