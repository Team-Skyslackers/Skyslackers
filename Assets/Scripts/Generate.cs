using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public TextAsset songfile;
    public float distance_from_player = 120, bolt_speed = 1; // bolt speed = unit distance travelled per frame
    Song currentSong;
    public GameObject beam;
    public float max_bolt_x = 10, max_bolt_y = 10;
    float StartTime, NextBoltTime;
    string NextBoltType;
    int totalBolts;
    void Start()
    {
        currentSong = new Song(songfile, distance_from_player, bolt_speed);
        Debug.Log(currentSong.raw_file);
        StartTime = Time.time;
        NextBoltTime = StartTime + currentSong.GetTime();
        NextBoltType = currentSong.GetBoltType();
        currentSong.PrepareNext();
    }

    void Update()
    {
        if (Time.time > NextBoltTime && NextBoltType != "end") {
            InstantiateAtPosition(NextBoltType.ToCharArray()[0]);

            NextBoltTime = StartTime + currentSong.GetTime();
            NextBoltType = currentSong.GetBoltType();
            currentSong.PrepareNext();
        }
    }

    void InstantiateAtPosition(char pos)
    {
        // assume 123QEASD cooresponds to 8 possible positions on screen.
        float x, y;
        if (pos == 'Q' || pos == 'A' || pos == 'Z')
            x = -max_bolt_x;
        else if (pos == 'W' || pos == 'X')
            x = 0;
        else
            x = max_bolt_x;

        if (pos == 'Q' || pos == 'W' || pos == 'E')
            y = max_bolt_y;
        else if (pos == 'A' || pos == 'D')
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

    public float GetTime()
    {
        if (current_line < line_count)
        {
            string[] timeComponent = each_line[current_line].Split(',')[0].Split('/');
            // assume time data follow the following convention:
            //                   7394761/720000
            // which corresponds to seconds
            float generationTime = float.Parse(timeComponent[0])
                / float.Parse(timeComponent[1])
                - distance_from_player / bolt_speed / 60;
            return generationTime;
        }
        else
            return 10000;
    }

    public float GetAdjustedTime()
    {
        if (current_line < line_count)
        {
            string[] timeComponent = each_line[current_line].Split(',')[0].Split(':');
            // assume time data follow the following convention:
            //                   03:26:59
            //         minutes : seconds : per 60 seconds
            float generationTime = float.Parse(timeComponent[0]) * 60
                + float.Parse(timeComponent[1])
                + float.Parse(timeComponent[2]) / 60
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