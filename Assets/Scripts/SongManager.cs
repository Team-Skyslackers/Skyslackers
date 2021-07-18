using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using WebSocketSharp;

public class SongManager : MonoBehaviour
{
    static public TextAsset musicMap;
    static public AudioClip musicFile;
    
    static public string scenename;

    WebSocket ws;

    public TextAsset ms;
    public AudioClip ac;

    void Start()
    {
        musicMap = null;
        musicFile = null;
        ws = new WebSocket("ws://localhost:18080");
        ws.ConnectAsync();
        ws.OnMessage += (sender, message) =>
        {   
            Debug.Log(message);
            if (message.Data.Substring(0, 13) == "musicselected")
            {
                string URLinfo = message.Data.Substring(15);
                string mp3URL = URLinfo.Split(' ')[0];
                string csvURL = URLinfo.Split(' ')[1];
                StartCoroutine(LoadGameFiles(mp3URL, csvURL));
            }
        };
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
        ws.Close();
        SceneManager.LoadScene(scenename);
    }

    public void setSong(string musicName)
    {
        musicMap = Resources.Load<TextAsset>("Music/" + musicName + "csv");
        musicFile = Resources.Load<AudioClip>("Music/" + musicName);
        SceneManager.LoadScene(scenename);
        // SceneManager.LoadScene("Game2",LoadSceneMode.Additive);
    }
}
