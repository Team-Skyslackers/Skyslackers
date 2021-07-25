using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Firebase.Auth;
//using Firebase.Storage;
//using Firebase.Database;



public class GameSummary : MonoBehaviour
{
    static public int perfect1, good1, missed1;
    static public int perfect2, good2, missed2;
    public Text totalScore1, perfectText1, goodText1, missedText1;
    public Text totalScore2, perfectText2, goodText2, missedText2;
    public int player;
    public GameObject SummaryCanvas;
    public GameObject WS_manager1;
    public GameObject WS_manager2;

    public struct GameResults
    {
        public string uid;
        public string music;
        public int score;
        public int perfect;
        public int good;
        public int missed;
        public string dateAndTimeUTC;
        public GameResults(int player, int _score, int _perfect, int _good, int _missed)
        {
            uid = (player == 1)? WS_Client.UID1:WS_Client.UID2;
            music = SongManager.songname;
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
        perfect1 = 0;
        good1 = 0;
        missed1 = 0;
        perfect2 = 0;
        good2 = 0;
        missed2 = 0;
        // test code
        Generate.musicFile.time = Generate.musicFile.clip.length-10;
        Debug.Log("settime");
    }

    void Update()
    {

        Debug.Log(Generate.music_current_time.ToString("N") + " / " + Generate.totalMusicLength.ToString("N"));
        if (Generate.music_current_time >= Generate.totalMusicLength - 1 && showingSummary == false)
        {
            // only run once when the game ends
            Cursor.visible = true;
            showingSummary = true;
            if (player == 1) {
                totalScore1.text = Generate.myScore1.ToString();
                perfectText1.text = perfect1.ToString();
                goodText1.text = good1.ToString();
                missedText1.text = missed1.ToString();
                Debug.Log("1");
                Debug.Log(Generate.myScore1);
                Debug.Log(perfect1);
                Debug.Log(good1);
                Debug.Log(missed1);
                totalScore2.text = Generate.myScore2.ToString();
                perfectText2.text = perfect2.ToString();
                goodText2.text = good2.ToString();
                missedText2.text = missed2.ToString();
                Debug.Log("2");
                SummaryCanvas.SetActive(true);
            }


            if (WS_manager1 != null)
            {
                GameResults gameResults1 = new GameResults(1, Generate.myScore1, perfect1, good1, missed1);
                WS_manager1.GetComponent<WS_Client>().ws.Send("Summary " + JsonUtility.ToJson(gameResults1).ToString());
            }

            if (WS_manager2 != null)
            {
                GameResults gameResults2 = new GameResults(2, Generate.myScore2, perfect2, good2, missed2);
                WS_manager2.GetComponent<WS_Client>().ws.Send("Summary " + JsonUtility.ToJson(gameResults2).ToString());
            }
    
            
        }
    }
}
