using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipHandler : MonoBehaviour
{
	[SerializeField] float MovementSpeed = 5;

	// TODO: Convert this to an array/list of the currently held shooters
	private IShooter _shooter;

	private int movementX;
	private int movementY;

	void Start()
	{
		_shooter = new SimpleShooter();
	}

	void FixedUpdate()
	{
		transform.Translate(movementX * MovementSpeed * Time.fixedDeltaTime, movementY * MovementSpeed * Time.fixedDeltaTime, 0, Space.Self);
	}

	void Update()
	{
		var up = KeyCode.UpArrow;
		var down = KeyCode.DownArrow;
		var left = KeyCode.LeftArrow;
		var right = KeyCode.RightArrow;
		var shoot = KeyCode.Space;

		if (Input.GetKey(up)) movementX = -1;
		else if (Input.GetKey(down)) movementX = 1;
		else movementX = 0;

		if (Input.GetKey(left)) movementY = -1;
		else if (Input.GetKey(right)) movementY = 1;
		else movementY = 0;

		_shooter.FromCurrentShootingState(Input.GetKeyDown(shoot), Input.GetKeyUp(shoot), Input.GetKey(shoot), gameObject);
	}
}
