using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop_up : MonoBehaviour
{
    // Start is called before the first frame update
    public float init_time;
    public Vector3 origin;
    public float pop_speed = 0.5f;

    void Start()
    {
        init_time = Time.time;
        origin = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position[1] < origin[1]+2){
            gameObject.transform.Translate(0f,pop_speed,0f);
        }

        if (Time.time - init_time > 0.5) {
            Destroy(this.gameObject);
        }
    }
}
