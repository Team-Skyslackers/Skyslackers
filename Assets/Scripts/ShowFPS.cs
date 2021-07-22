using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    float startTime;
    int framesCount;
    Text FPSdisplay;

    // Start is called before the first frame update
    void Start()
    {
        framesCount = 0;
        startTime = Time.time;
        FPSdisplay = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        framesCount++;
        if (Time.time - startTime > 0.5f)
        {
            FPSdisplay.text = (((float)framesCount) / (Time.time - startTime)).ToString("N") + " FPS";
            framesCount = 0;
            startTime = Time.time;
        }
    }
}
