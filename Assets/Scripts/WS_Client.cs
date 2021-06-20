using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WS_Client : MonoBehaviour
{
    public GameObject lightsaber;
    private float x = 0, y = 0, z = 0;
    private float raw_x = 0, raw_y = 0, raw_z = 0;
    private float pos_x = 0, pos_y = 0;
    WebSocket ws;
    private void Start()
    {
        ws = new WebSocket("ws://localhost:8080");
        ws.Connect();
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);
            raw_x = float.Parse(e.Data.Split(' ')[1]);
            raw_y = float.Parse(e.Data.Split(' ')[0]);
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
        lightsaber.transform.position = new Vector3(pos_x/9.0f, pos_y/9.0f,0);
        // lightsaber.transform.Translate(-0.01f,0f,0f);
        // float posx = 0, posy = 0;
        // if(x <= 90){
        //     posx = x * 20.0 / 90.0;
        // }
        // lightsaber.transform.position = 
    }
}
