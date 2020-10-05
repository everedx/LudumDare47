using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] List<GameObject> recordRows;


    // Start is called before the first frame update
    void Start()
    {
        List<ScoreRecord> scores = GameManager.instance.GetAllRecords();
        scores = scores.OrderBy(o => o.score).ToList();
        scores.Reverse();


        for (int i = 0; i < 10; i++)
        {
            recordRows[i].transform.GetChild(0).GetComponent<Text>().text = scores[i].name;
            recordRows[i].transform.GetChild(1).GetComponent<Text>().text = scores[i].score.ToString();
        }
    }

}
