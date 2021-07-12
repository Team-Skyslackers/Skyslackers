using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayUID : MonoBehaviour
{
    public Text UIDText;
    public GameObject WS_manager;

    // Update is called once per frame
    void Update()
    {
        UIDText.text = WS_Client.UID;
    }
}
