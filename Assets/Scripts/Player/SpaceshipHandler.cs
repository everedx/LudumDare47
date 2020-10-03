using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class SpaceshipHandler : MonoBehaviour
{
	[SerializeField] float MovementSpeed = 5;

	// TODO: Convert this to an array/list of the currently held shooters
	private IShooter _shooter;
	private Camera _mainCamera;
	private Rect _halfSpriteRect;

	private int movementX;
	private int movementY;

	void Start()
	{
		_shooter = new SimpleShooter();
		_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		_halfSpriteRect = new Rect(GetComponent<SpriteRenderer>().sprite.rect);
		_halfSpriteRect.width /= 2;
		_halfSpriteRect.height /= 2;
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

		var screenPos = _mainCamera.WorldToScreenPoint(transform.position);

		// Pressing Right creates movement in the positive Y coordinates... makes these conditions a bit weird, fix it somehow if it becomes too annoying
		if (screenPos.x - _halfSpriteRect.width < 0 && movementY < 0) movementY = 0;
		else if (screenPos.x + _halfSpriteRect.width > _mainCamera.pixelWidth && movementY > 0) movementY = 0;
		if (screenPos.y - _halfSpriteRect.height < 0 && movementX > 0) movementX = 0;
		else if (screenPos.y + _halfSpriteRect.height > _mainCamera.pixelHeight && movementX < 0) movementX = 0;

		_shooter.FromCurrentShootingState(Input.GetKeyDown(shoot), Input.GetKeyUp(shoot), Input.GetKey(shoot), gameObject);
	}
}
