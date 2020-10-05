using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineFollower : MonoBehaviour
{
	[SerializeField] PathCreator pathCreator;
	[SerializeField] float secondsPerLap = 80;
	[SerializeField] float rewindTime = 5f;
	[SerializeField] float stoppingTime = 2f;
	[SerializeField] AudioClip rewindClip;

	private List<RoundParallax> parallaxList;
	private List<float> parallaxSpeedList;

	float distanceTravelled;
	float speed;
	bool deathSequence;
	private DeathStates dState;
	private float initialSpeed;
	private float rewindSpeed;
	float smoothRewindSpeed;
	MusicManager musicManager;

	private float timer;

	private enum DeathStates
	{
		Stopping,
		Rewinding,
		LoadingScene
	}


	private void Start()
	{
		smoothRewindSpeed = 0;
		timer = 0;
		deathSequence = false;
		speed = pathCreator.path.length / secondsPerLap;
		parallaxList = GameObject.FindObjectsOfType<RoundParallax>().ToList();
		initialSpeed = speed;
		musicManager = Camera.main.GetComponent<MusicManager>();
		parallaxSpeedList = new List<float>();
		for (int i = 0; i < parallaxList.Count; i++)
		{
			parallaxSpeedList.Add(parallaxList[i].Speed);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!deathSequence)
		{
			distanceTravelled += -speed * Time.deltaTime;
			transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
			transform.up = -pathCreator.path.GetDirectionAtDistance(distanceTravelled);
		}
		else
		{
			timer += Time.deltaTime;
			switch (dState)
			{
				case DeathStates.Stopping:
					speed = Mathf.Lerp(speed,0, timer / stoppingTime);
					musicManager.setVolume(Mathf.Lerp(musicManager.GetMusicVolume(), 0, timer / stoppingTime));
					foreach(RoundParallax par in parallaxList)
						par.Speed = Mathf.Lerp(par.Speed, 0,timer/stoppingTime );
					if (speed == 0)
					{
						dState = DeathStates.Rewinding;
						rewindSpeed = distanceTravelled / rewindTime;
						timer = 0;
						musicManager.PlaySFX(rewindClip);
				
					}
					break;
				case DeathStates.Rewinding:
					
					Rewind();
					
					if (distanceTravelled >= 0)
						dState = DeathStates.LoadingScene;
					break;
				case DeathStates.LoadingScene:
					musicManager.RestartSynchronization();
					GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().RestartCharacter();
					GameObject.FindObjectOfType<ItemGenerator>().GetComponent<ItemGenerator>().RestartLevelDifficulty();
					GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().ResetScore();
					//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					for (int i = 0; i < parallaxList.Count; i++)
					{
						parallaxList[i].Speed = parallaxSpeedList[i];
					}
					deathSequence = false;
					speed = initialSpeed;
					break;
			}
		}

	}


	public Vector2 GetUpVector()
	{
		return transform.up;
	}

	public float GetSecondsPerLap()
	{
		return secondsPerLap;
	}

	public void Rewind()
	{
		
		if (timer < 0.5f)
		{
			smoothRewindSpeed = Mathf.SmoothStep(0,rewindSpeed, timer / 0.5f);
		}

		if (timer > rewindTime - 0.5f)
		{
			smoothRewindSpeed = Mathf.SmoothStep(rewindSpeed, 0 , timer - (rewindTime - 0.5f) / 0.5f);
		}



		distanceTravelled += -smoothRewindSpeed *  Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
		transform.up = -pathCreator.path.GetDirectionAtDistance(distanceTravelled);
	}


	public void StartDeathSequence()
	{
		deathSequence = true;
		GameObject.FindObjectOfType<ItemGenerator>().GetComponent<ItemGenerator>().StopGeneration();
		GameObject.FindObjectOfType<ObjectDestroyer>().GetComponent<ObjectDestroyer>().DestroyScene();
		dState = DeathStates.Stopping;
		timer = 0;
	}

}
