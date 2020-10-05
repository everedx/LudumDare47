﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour
{
	[System.Serializable]
	public class GeneratedItem
	{
		public int chance;
		public GameObject item;
	}

	[SerializeField] GeneratedItem[] Items;
    [SerializeField] float timeToSpawnNewItem = 2;
	[SerializeField] int initialLevel = 1;
	[SerializeField] int finalLapNumberSize = 36;
	[SerializeField] float decreaseLapSizeEvery = 0.05f;
	float timer;
	int chanceSum;
	float secondsPerLap;
	int currentLevel;
	bool updatingLap;
	Text lapDisplayer;
	float lastChangedFontSize;
	float levelTimer;
	bool generationEnabled;

    // Start is called before the first frame update
    void Start()
    {
		generationEnabled = true;
		levelTimer = 0;
        timer = 0;
		chanceSum = Items.Sum(i => i.chance);
		timer = timeToSpawnNewItem; // Spawn one at the beginning and wait... good for testing items
		secondsPerLap = GameObject.FindGameObjectWithTag("Follower").GetComponent<LineFollower>().GetSecondsPerLap();
		lapDisplayer = GameObject.FindGameObjectWithTag("LapDisplayer").GetComponent<Text>();
		currentLevel = initialLevel;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
		levelTimer += Time.deltaTime;
		var prevLevel = currentLevel;
		currentLevel = initialLevel + int.Parse(Math.Floor(levelTimer / secondsPerLap).ToString());
		if (prevLevel != currentLevel) updatingLap = true;
		UpdateLap();

        if (timer > timeToSpawnNewItem)
        {
			// +1 because max in range is exclusive
			var val = UnityEngine.Random.Range(1, chanceSum + 1);

			Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
			spawnPosition = spawnPosition + Camera.main.transform.right * 20 + Camera.main.transform.up * UnityEngine.Random.Range(-10, 10);

			var newItem = GetObjectForVal(val);
			var levelable = newItem.GetComponent<ILevelable>();
			if (levelable != null) levelable.SetLevel(currentLevel);

			if(generationEnabled)
				Instantiate(newItem, spawnPosition, Quaternion.identity);

            timer = 0;
        }

    }

	void UpdateLap()
	{
		if (!updatingLap) return;

		if (lapDisplayer.fontSize == finalLapNumberSize && int.Parse(lapDisplayer.text) == currentLevel - 1)
		{
			lapDisplayer.text = currentLevel.ToString();
			lapDisplayer.fontSize = 3 * finalLapNumberSize;
			lastChangedFontSize = Time.time;
		}
		else if (lapDisplayer.fontSize == finalLapNumberSize && int.Parse(lapDisplayer.text) == currentLevel)
		{
			updatingLap = false;
			return;
		}

		if (Time.time - lastChangedFontSize > decreaseLapSizeEvery) lapDisplayer.fontSize--;
		if(lapDisplayer.fontSize <= finalLapNumberSize)
		{
			updatingLap = false;
			lapDisplayer.fontSize = finalLapNumberSize;
			lapDisplayer.text = currentLevel.ToString();
		}
	}

	GameObject GetObjectForVal(int val)
	{
		int cummulative = 1;

		foreach(var i in Items)
		{
			if (val < cummulative + i.chance)
				return i.item;

			cummulative += i.chance;
		}

		return null;
	}

	public void StopGeneration()
	{
		generationEnabled = false;
	}

	public void RestartLevelDifficulty()
	{
		generationEnabled = true;
		levelTimer = 0;
		updatingLap = true;
		currentLevel = 1;
	}

}
