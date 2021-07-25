using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{   
    public string newbackground;
    // Start is called before the first frame update
    public void setbackground()
    {
        PlayerPrefs.SetString("background", newbackground);
	    PlayerPrefs.Save();
        Debug.Log("saved");
    }
}
