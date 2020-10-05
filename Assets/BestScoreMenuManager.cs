using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BestScoreMenuManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] TextMeshProUGUI scoreTextName;
    [SerializeField] Button button;
    [SerializeField] LineFollower follower;

    private void Update()
    {
        if (string.IsNullOrEmpty(scoreText.text))
            button.enabled = false;
        else
            button.enabled = true;
            
    }

    public void LoadData()
    {
        scoreText.text = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score.ToString();
    }


    public void SetScore()
    {

        GameManager.instance.AddNewRecord(new ScoreRecord() { name= scoreTextName.text,score = int.Parse(scoreText.text)});
        
        follower.ContinueSequence();
    }
}
