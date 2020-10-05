using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollower : MonoBehaviour
{
	[SerializeField] PathCreator pathCreator;
	float speed;
	[SerializeField] float secondsPerLap = 80;
	float distanceTravelled;

	Vector2 initialPoint;

	private void Start()
	{
		initialPoint = pathCreator.path.GetPointAtDistance(0);
		speed = pathCreator.path.length / 80;
		
	}

	// Update is called once per frame
	void Update()
	{
		distanceTravelled += -speed * Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
		transform.up = -pathCreator.path.GetDirectionAtDistance(distanceTravelled);
	}


	public Vector2 GetUpVector()
	{
		return transform.up;
	}

}
