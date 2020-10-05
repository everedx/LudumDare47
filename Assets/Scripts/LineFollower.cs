using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineFollower : MonoBehaviour
{
	[SerializeField] PathCreator pathCreator;	
	[SerializeField] float secondsPerLap = 80;

	private List<RoundParallax> parallaxList;	
	float distanceTravelled;
	float speed;
	bool deathSequence;
	private DeathStates dState;

	private enum DeathStates
	{
		Stopping,
		Rewinding,
		LoadingScene
	}


	private void Start()
	{
		deathSequence = false;
		speed = pathCreator.path.length / secondsPerLap;
		parallaxList = GameObject.FindObjectsOfType<RoundParallax>().ToList();
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
			switch (dState)
			{
				case DeathStates.Stopping:
					speed = Mathf.Lerp(speed,0,0.1f);
					foreach(RoundParallax par in parallaxList)
						par.Speed = Mathf.Lerp(par.Speed, 0, 1f * Time.deltaTime);
					break;
				case DeathStates.Rewinding:
					///speed = Mathf.Lerp(speed, 0, 0.1f);
					break;
				case DeathStates.LoadingScene:
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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


	public void StartDeathSequence()
	{
		deathSequence = true;
		dState = DeathStates.Stopping;
	}

}
