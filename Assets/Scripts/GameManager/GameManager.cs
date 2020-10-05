using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Data;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : GameManagerBase<GameManager, GameDataStore>
{
    protected override void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        base.Awake();
    }

    public void AddRecord(ScoreRecord score)
    {
        m_DataStore.AddRecord(score);
    }

    public int GetBestScore()
    {
        return m_DataStore.GetBestMark();
    }
    public int GetLowestBestScore()
    {
        return m_DataStore.GetLowestBestMark();
    }

    public void AddNewRecord(ScoreRecord score)
    {
        m_DataStore.DeleteLastRecord();
        m_DataStore.AddRecord(score);
        SaveData();
    }


    public List<ScoreRecord> GetAllRecords()
    {
        return m_DataStore.GetAllRecords();
    }


    private void InitializeCollections()
    {
        if (m_DataStore.scores.Count == 0)
        {
            m_DataStore.scores.Add(new ScoreRecord() { name = "AAA", score = 10000 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "BBB", score = 5000 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "CCC", score = 4000 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "DDD", score = 3000 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "FFF", score = 2000 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "GGG", score = 1000 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "HHH", score = 300 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "III", score = 200 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "JJJ", score = 100 });
            m_DataStore.scores.Add(new ScoreRecord() { name = "LLL", score = 100 });
        }
    }


}
