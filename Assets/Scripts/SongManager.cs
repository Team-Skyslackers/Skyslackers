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

    // IEnumerator DownloadFile(string url, string filename, string saveDir = "")
    // {
    //     var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
    //     string path = Path.Combine(Application.persistentDataPath + saveDir, filename);
    //     uwr.downloadHandler = new DownloadHandlerFile(path);
    //     yield return uwr.SendWebRequest();
    //     if (uwr.result != UnityWebRequest.Result.Success)
    //         Debug.LogError(filename + " failed to download with error: " + uwr.error);
    //     else
    //         Debug.Log(filename + " successfully downloaded and saved to " + path);
    // }

    IEnumerator LoadGameFiles(string pathToMusic, string pathToMap)
    {
        Debug.Log("loading music & map");
        Debug.Log(Application.persistentDataPath + pathToMap);
        while (!System.IO.File.Exists(Application.persistentDataPath + pathToMusic) ||
            !System.IO.File.Exists(Application.persistentDataPath + pathToMap))
        {
            yield return null;
        }
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip("https://firebasestorage.googleapis.com/v0/b/test-7f7c0.appspot.com/o/musicFile%2Fsong1.mp3?alt=media&token=f1f5732c-8309-4209-8763-988f2003cb34", AudioType.MPEG)) {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError) {
                Debug.Log(uwr.error);
            }
            else {
                Debug.Log("File://"+Application.persistentDataPath + pathToMusic);
                // Get downloaded asset bundle
                musicFile = DownloadHandlerAudioClip.GetContent(uwr);
                // musicFile.LoadAudioData();
                // Something with the texture e.g.
                // store it to later access it by fileName
                // musicFile = music_file;
                Debug.Log(musicFile);
                Debug.Log(musicFile.length);
            }
        // musicFile = Resources.Load<AudioClip>(Application.persistentDataPath + pathToMusic);
        // musicMap = Resources.Load<TextAsset>(Application.persistentDataPath + pathToMap);
        // SceneManager.LoadScene("Game");
        }
        using (UnityWebRequest uwr = UnityWebRequest.Get("File://"+Application.persistentDataPath + pathToMap)) {
            yield return uwr.SendWebRequest();
            if (uwr.isNetworkError || uwr.isHttpError) {
                Debug.Log(uwr.error);
            }
            else {
                Debug.Log(uwr);
                // Get downloaded asset bundle
                // Something with the texture e.g.
                // store it to later access it by fileName
                musicMap = new TextAsset(uwr.downloadHandler.text);;
                Debug.Log(musicMap);
            }
        // musicFile = Resources.Load<AudioClip>(Application.persistentDataPath + pathToMusic);
        // musicMap = Resources.Load<TextAsset>(Application.persistentDataPath + pathToMap);
            // SceneManager.LoadScene("Game");
        }
    }

    public void setSong()
    {
        
        if (ms == null || ac == null)
        {
            //StartCoroutine(DownloadFile("https://en.images.guru/images/2021/07/08/38nps.png", "38nps.png", "/Assets/"));

            // string songURL = "https://firebasestorage.googleapis.com/v0/b/test-7f7c0.appspot.com/o/musicFile%2Fsong1.wav?alt=media&token=c18117cc-dcac-41a9-b66a-35eb201d9bd4";
            // StartCoroutine(DownloadFile(songURL, "song1.wav", "/Music/"));
            // string songCsvURL = "https://firebasestorage.googleapis.com/v0/b/test-7f7c0.appspot.com/o/musicFile%2Fsong1.csv?alt=media&token=90e8c03e-e314-4727-ab27-ecefad99cfac";
            // StartCoroutine(DownloadFile(songCsvURL, "song1.csv", "/Music/"));

            StartCoroutine(LoadGameFiles("/Music/song1.wav", "/Music/song1.csv"));

            if (musicMap != null && musicFile != null)
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
