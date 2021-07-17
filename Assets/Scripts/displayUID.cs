using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayUID : MonoBehaviour
{
    public Text UIDText;
    public GameObject WS_manager;
    public int player;

    // Update is called once per frame
    void Update()
    {
        UIDText.text = (player == 1)? WS_Client.UID1:WS_Client.UID2;
    }
}
