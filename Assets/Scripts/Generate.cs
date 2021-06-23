using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public TextAsset songfile;
    public float distance_from_player = 120, bolt_speed = 1;
    Song currentSong;
    public GameObject beam;
    public float max_bolt_x = 4, max_bolt_y = 4;
    float StartTime, NextBoltTime;
    string NextBoltType;
    int totalBolts;
    void Start()
    {
        currentSong = new Song(songfile, distance_from_player, bolt_speed);
        Debug.Log(currentSong.raw_file);
        StartTime = Time.time;
        NextBoltTime = StartTime + currentSong.GetAdjustedTime();
        NextBoltType = currentSong.GetBoltType();
        currentSong.PrepareNext();
    }

    void Update()
    {
        if (Time.time > NextBoltTime && NextBoltType != "end") {
            InstantiateAtPosition(NextBoltType.ToCharArray()[0]);

            NextBoltTime = StartTime + currentSong.GetAdjustedTime();
            NextBoltType = currentSong.GetBoltType();
            currentSong.PrepareNext();
        }
    }

    void InstantiateAtPosition(char pos)
    {
        // assume 123QEASD cooresponds to 8 possible positions on screen.
        float x, y;
        if (pos == '1' || pos == 'Q' || pos == 'A')
            x = -max_bolt_x;
        else if (pos == '2' || pos == 'S')
            x = 0;
        else
            x = max_bolt_x;

        if (pos == '1' || pos == '2' || pos == '3')
            y = max_bolt_y;
        else if (pos == 'Q' || pos == 'E')
            y = 0;
        else
            y = -max_bolt_y;

        Instantiate(beam, new Vector3(x, y, distance_from_player), Quaternion.identity);
    }
}

public class Song
{
    public string raw_file;
    string[] each_line;
    int line_count;
    int current_line = 0;
    public float distance_from_player = 120, bolt_speed = 1;

    public Song(TextAsset songFile, float distanceFromPlayer, float boltSpeed)
    {
        raw_file = songFile.text;
        each_line = raw_file.Split('\n');
        line_count = each_line.Length;
        distance_from_player = distanceFromPlayer;
        bolt_speed = boltSpeed;
    }

    public string NextLine()
    {
        if (current_line < line_count)
            return each_line[current_line++];
        else
            return "";
    }

    public float GetAdjustedTime()
    {
        if (current_line < line_count)
        {
            string[] timeComponent = each_line[current_line].Split(',')[0].Split(':');
            // assume time data follow the following convention:
            //                   03:26:59
            //         minutes : seconds : per 60 seconds
            float generationTime = int.Parse(timeComponent[0]) * 60
                + int.Parse(timeComponent[1])
                + (float)int.Parse(timeComponent[2]) / 60
                - distance_from_player / bolt_speed / 60;

            //Debug.Log(generationTime.ToString("N"));
            return generationTime;
        }
        else
            return 10000;
    }

    public string GetBoltType()
    {
        if (current_line < line_count)
        {
            Debug.Log(each_line[current_line].Split(',')[1]);
            return each_line[current_line].Split(',')[1];
        }
        else
            return "end";
    }

    public void PrepareNext()
    {
        if (current_line < line_count)
            current_line++;
    }
}