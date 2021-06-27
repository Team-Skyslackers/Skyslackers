using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSummary : MonoBehaviour
{
    static public int perfect, good, missed;
    public Text totalScore, perfectText, goodText, missedText;
    public GameObject SummaryCanvas;

    void Update()
    {
        //Debug.Log(Generate.music_current_time.ToString("N") + " / " + Generate.totalMusicLength.ToString("N"));
        if (Generate.music_current_time >= Generate.totalMusicLength - 1)
        {
            totalScore.text = Generate.myScore.ToString();
            perfectText.text = perfect.ToString();
            goodText.text = good.ToString();
            missedText.text = missed.ToString();
            SummaryCanvas.SetActive(true);
        }
    }
}
