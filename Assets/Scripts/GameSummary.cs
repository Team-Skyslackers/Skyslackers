using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Storage;
using Firebase.Database;



public class GameSummary : MonoBehaviour
{
    static public int perfect, good, missed;
    public Text totalScore, perfectText, goodText, missedText;
    public GameObject SummaryCanvas;
    public GameObject WS_manager;
    public struct GameResults
    {
        public string uid;
        public string music;
        public int score;
        public int perfect;
        public int good;
        public int missed;
        public string dateAndTimeUTC;
        public GameResults(int _score, int _perfect, int _good, int _missed)
        {
            uid = WS_Client.UID;
            music = SongManager.musicFile.name;
            score = _score;
            perfect = _perfect;
            good = _good;
            missed = _missed;
            dateAndTimeUTC = DateTime.UtcNow.ToString("s");
        }
    }

    static public bool showingSummary = false;

    private void Start()
    {
        showingSummary = false;
        perfect = 0;
        good = 0;
        missed = 0;

        // test code
        Generate.musicFile.time = Generate.musicFile.clip.length-10;
    }

    void Update()
    {
        //Debug.Log(Generate.music_current_time.ToString("N") + " / " + Generate.totalMusicLength.ToString("N"));
        if (Generate.music_current_time >= Generate.totalMusicLength - 1 && showingSummary == false)
        {
            // only run once when the game ends
            Cursor.visible = true;
            showingSummary = true;
            totalScore.text = Generate.myScore.ToString();
            perfectText.text = perfect.ToString();
            goodText.text = good.ToString();
            missedText.text = missed.ToString();
            SummaryCanvas.SetActive(true);

            GameResults gameResults = new GameResults(Generate.myScore, perfect, good, missed);
            WS_manager.GetComponent<WS_Client>().ws.Send("Summary " + JsonUtility.ToJson(gameResults).ToString()) ;
        }
    }
}
