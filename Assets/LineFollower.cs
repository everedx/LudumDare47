using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollower : MonoBehaviour
{
	public PathCreator pathCreator;
	public float speed = 7;
	float distanceTravelled;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		distanceTravelled += -speed * Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
		transform.up = -pathCreator.path.GetDirectionAtDistance(distanceTravelled);
	}
}
