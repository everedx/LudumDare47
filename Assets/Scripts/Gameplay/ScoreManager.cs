using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	private TextMeshProUGUI text;
	int score = 0;

	public int Score { get => score;}

	void Start()
    {
		text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void AddScore(int amount)
	{
		score += amount;
		text.text = score.ToString();
	}

	public void ResetScore()
	{
		score = 0;
		text.text = score.ToString();
	}
}
