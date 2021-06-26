using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WS_Client : MonoBehaviour
{
    public GameObject lightsaber;
    private float x = 0, y = 0, z = 0;
    private float raw_x = 0, raw_y = 0;
    private float raw_z = 0;
    private float pos_x = 0, pos_y = 0;
    WebSocket ws;


    // calculate max angular velocity over past 10 frames
    float[] past_av = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int past_av_ind = 0;
    Vector2 deltaRotation;
    Vector2 last_axy = new Vector2(0, 0);
    static public float blade_av; // take max over past 10 frame


    private void Start()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            //Debug.Log(e.Data);
            raw_x = float.Parse(e.Data.Split(' ')[1]);
            raw_y = float.Parse(e.Data.Split(' ')[0]);
            raw_z = float.Parse(e.Data.Split(' ')[2]);
            y = -raw_y;
            x = 90-raw_x;
            //z = -float.Parse(e.Data.Split(' ')[2]);
        };
    }
    void Update()
    {
        lightsaber.transform.rotation = Quaternion.Euler(x, y, z);
        pos_x = (raw_y < 180)? ((raw_y > 90)? raw_y - 180.0f:-raw_y): ((raw_y < 270)? raw_y - 180.0f :360.0f - raw_y);
        pos_y = (raw_x > 90)? 180.0f-raw_x:((raw_x<-90)? -180.0f - raw_x:raw_x);
        pos_x = (raw_x > 90 || raw_x < -90)? -pos_x:pos_x;
        new_av(pos_x, pos_y);
        // Debug.Log(blade_av.ToString("N"));
        lightsaber.transform.position = new Vector3(pos_x/9.0f, pos_y/9.0f,0);
    }


    void new_av(float _x, float _y)
    {
        deltaRotation = new Vector2(_x, _y) - last_axy;
        last_axy = new Vector2(_x, _y);

        past_av[past_av_ind] = deltaRotation.magnitude;
        past_av_ind = (past_av_ind == 9) ? 0 : past_av_ind + 1;

        float temp = 0;
        foreach (float val in past_av)
            temp = (val > temp) ? val : temp;

        blade_av = temp;
    }
}
