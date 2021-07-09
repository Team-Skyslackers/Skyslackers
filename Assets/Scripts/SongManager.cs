using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

public class SongManager : MonoBehaviour
{
    static public TextAsset musicMap;
    static public AudioClip musicFile;
    //public void setSong(TextAsset Music_Map, AudioSource Music_File)


    public TextAsset ms;
    public AudioClip ac;

    void Start()
    {
        musicMap = null;
        musicFile = null;
    }

    //IEnumerator LoadAC(string URL)
    //{
    //    Debug.Log("started downloading music file");
    //    UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(URL, AudioType.MPEG);
    //    yield return request.SendWebRequest();
    //    if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
    //    {
    //        DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)request.downloadHandler;
    //        if (dlHandler.isDone)
    //        {
    //            Debug.Log("Music file download complete");
    //            musicFile = dlHandler.audioClip;
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log(request.error);
    //    }
    //}

    //IEnumerator LoadMS(string URL)
    //{
    //    Debug.Log("started downloading map");
    //    UnityWebRequest request = UnityWebRequest.Get(URL);
    //    yield return request.SendWebRequest();
    //    if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
    //    {
    //        DownloadHandler dlHandler = request.downloadHandler;
    //        if (dlHandler.isDone)
    //        {
    //            Debug.Log("csv interpreted as: " + dlHandler.text);
    //            musicMap = new TextAsset(dlHandler.text);
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log(request.error);
    //    }
    //}

    IEnumerator DownloadFile(string url, string filename, string saveDir = "")
    {
        var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.persistentDataPath + saveDir, filename);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
            Debug.LogError(filename + " failed to download with error: " + uwr.error);
        else
            Debug.Log(filename + " successfully downloaded and saved to " + path);
    }


    public void setSong()
    {
        
        if (ms == null || ac == null)
        {
            //StartCoroutine(DownloadFile("https://en.images.guru/images/2021/07/08/38nps.png", "38nps.png", "/Assets/"));

            string songURL = "https://firebasestorage.googleapis.com/v0/b/test-7f7c0.appspot.com/o/musicFile%2Fsong1.mp3?alt=media&token=f1f5732c-8309-4209-8763-988f2003cb34";
            StartCoroutine(DownloadFile(songURL, "song1.mp3", "/Music/"));
            string songCsvURL = "https://firebasestorage.googleapis.com/v0/b/test-7f7c0.appspot.com/o/musicFile%2Fsong1.csv?alt=media&token=90e8c03e-e314-4727-ab27-ecefad99cfac";
            StartCoroutine(DownloadFile(songCsvURL, "song1.csv", "/Music/"));

            musicFile = Resources.Load<AudioClip>(Application.persistentDataPath + "/Music/song1.mp3");
            musicMap = Resources.Load<TextAsset>(Application.persistentDataPath + "/Music/song1.csv");

            if (musicMap != null || musicFile != null)
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                Debug.Log("Music and/or map file not found");
            }
        }
        else
        {
            musicMap = ms;
            musicFile = ac;
            SceneManager.LoadScene("Game");
        }
    }

    
}
