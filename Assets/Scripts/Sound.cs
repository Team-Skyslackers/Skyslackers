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
        Debug.Log("Sound played");
        source.Play();
        
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
