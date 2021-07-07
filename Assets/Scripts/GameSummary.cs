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
        public int perfect;
        public int good;
        public int missed;
        public int total;
        public string dateAndTimeUTC;
        public GameResults(int _total, int _perfect, int _good, int _missed)
        {
            uid = "placeholder";
            total = _total;
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
