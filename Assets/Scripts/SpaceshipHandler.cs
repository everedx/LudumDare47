using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipHandler : MonoBehaviour
{
	[SerializeField] float MovementSpeed = 5;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	void FixedUpdate()
	{
		int movementX = 0;
		if (Input.GetKey(KeyCode.UpArrow)) movementX = -1;
		else if (Input.GetKey(KeyCode.DownArrow)) movementX = 1;

		int movementY = 0;
		if (Input.GetKey(KeyCode.LeftArrow)) movementY = -1;
		else if (Input.GetKey(KeyCode.RightArrow)) movementY = 1;

		transform.Translate(movementX * MovementSpeed * Time.fixedDeltaTime, movementY * MovementSpeed * Time.fixedDeltaTime, 0, Space.Self);
	}
}
