using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongManager : MonoBehaviour
{
    static public TextAsset musicScore;
    static public AudioClip musicFile;
    //public void setSong(TextAsset Music_Score, AudioSource Music_File)

    public TextAsset ms;
    public AudioClip ac;

    public void setSong()
    {
        musicScore = ms;
        musicFile = ac;
        SceneManager.LoadScene("Game");
    }
}
