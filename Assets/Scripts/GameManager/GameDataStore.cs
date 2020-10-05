using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;
using System;
using UnityEngine.SocialPlatforms.Impl;
using System.Data;

public class GameDataStore : GameDataStoreBase
{


    public List<ScoreRecord> scores = new List<ScoreRecord>();


    public GameDataStore() : base()
    {
        scores.Add(new ScoreRecord() { name = "AAA", score = 10000});
        scores.Add(new ScoreRecord() { name = "BBB", score = 5000 });
        scores.Add(new ScoreRecord() { name = "CCC", score = 4000 });
        scores.Add(new ScoreRecord() { name = "DDD", score = 3000 });
        scores.Add(new ScoreRecord() { name = "FFF", score = 2000 });
        scores.Add(new ScoreRecord() { name = "GGG", score = 1000 });
        scores.Add(new ScoreRecord() { name = "HHH", score = 300 });
        scores.Add(new ScoreRecord() { name = "III", score = 200 });
        scores.Add(new ScoreRecord() { name = "JJJ", score = 100 });
        scores.Add(new ScoreRecord() { name = "LLL", score = 100 });
    }

    public void DeleteLastRecord()
    {
        ScoreRecord score = null;
        int minScore = int.MaxValue;
        foreach (ScoreRecord record in scores)
        {
            if (record.score < minScore)
            {
                minScore = record.score;
                score = record;
            }
                
        }
        scores.Remove(score);
    }

    public void AddRecord(ScoreRecord score)
    {
        scores.Add(score);
    }

    public int GetBestMark()
    {
        //return bestMark;
        int maxScore = 0;
        foreach (ScoreRecord record in scores)
        {
            if (record.score > maxScore)
                maxScore = record.score;
        }
        return maxScore;
    }

    public int GetLowestBestMark()
    {
        //return bestMark;
        int minScore = int.MaxValue; ;
        foreach (ScoreRecord record in scores)
        {
            if (record.score < minScore)
                minScore = record.score;
        }
        return minScore;
    }


    public List<ScoreRecord> GetAllRecords()
    {
        return scores;
    }

    public override void preSave()
    {
        Debug.Log("[GAME] Saving Game");

    }
    public override void postLoad()
    {
        Debug.Log("[GAME] Loaded Game");
    }




}
[Serializable]
public class ScoreRecord
{
    public int score;
    public string name;
}




