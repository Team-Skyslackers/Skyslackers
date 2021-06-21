using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float period = 2.0f;
    public float distance_from_player = 120.0f;
    public GameObject beam;
    public float max_bolt_x = 10, max_bolt_y = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime ) {
            nextActionTime += period;
            Instantiate(beam, new Vector3(Random.Range(-max_bolt_x, max_bolt_x), Random.Range(-max_bolt_y, max_bolt_y), distance_from_player), Quaternion.identity);
        }
    }
}
