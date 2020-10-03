using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollower : MonoBehaviour
{
	[SerializeField] PathCreator pathCreator;
	[SerializeField] float speed = 7;
	float distanceTravelled;



	// Update is called once per frame
	void Update()
	{
		distanceTravelled += -speed * Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
		transform.up = -pathCreator.path.GetDirectionAtDistance(distanceTravelled);
	}
}
