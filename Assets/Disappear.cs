using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    // Start is called before the first frame update
    public float init_time;

    void Start()
    {
        init_time = Time.time;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - init_time > 1) {
            Destroy(this.gameObject);
        }
    }
}
