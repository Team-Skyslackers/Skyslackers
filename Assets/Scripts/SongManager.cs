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


    public TextAsset ms;
    public AudioClip ac;

    void Start()
    {
        musicMap = null;
        musicFile = null;
    }

    IEnumerator LoadGameFiles(string pathToMusic, string pathToMap)
    {
        Debug.Log("loading music & map");
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(pathToMusic, AudioType.MPEG)) {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.ConnectionError ||
                uwr.result == UnityWebRequest.Result.ProtocolError) {
                Debug.Log(uwr.error);
            }
            else {
                //Debug.Log("File://"+Application.persistentDataPath + pathToMusic);
                // Get downloaded asset bundle
                musicFile = DownloadHandlerAudioClip.GetContent(uwr);
                // musicFile.LoadAudioData();
                // Something with the texture e.g.
                // store it to later access it by fileName
                // musicFile = music_file;
                Debug.Log(musicFile);
                Debug.Log(musicFile.length);
            }
        }
        using (UnityWebRequest uwr = UnityWebRequest.Get(pathToMap)) {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.ConnectionError ||
                uwr.result == UnityWebRequest.Result.ProtocolError) {
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
        }
        while (musicMap == null || musicFile == null)
        {
            yield return null;
        }
        SceneManager.LoadScene("Game");
    }

    public void setSong()
    {
        
        if (ms == null || ac == null)
        {
            string songURL = "https://firebasestorage.googleapis.com/v0/b/test-7f7c0.appspot.com/o/musicFile%2Fsong1.mp3?alt=media&token=f1f5732c-8309-4209-8763-988f2003cb34";
            string songCsvURL = "https://firebasestorage.googleapis.com/v0/b/test-7f7c0.appspot.com/o/musicFile%2Fsong1.csv?alt=media&token=90e8c03e-e314-4727-ab27-ecefad99cfac";

            StartCoroutine(LoadGameFiles(songURL, songCsvURL));

            //if (musicMap != null && musicFile != null)
            //{
            //   SceneManager.LoadScene("Game");
            //}
            //else
            //{
            //   Debug.Log("Music and/or map file not found");
            //}
        }
        else
        {
            musicMap = ms;
            musicFile = ac;
            SceneManager.LoadScene("Game");
        }
    }

    
}
