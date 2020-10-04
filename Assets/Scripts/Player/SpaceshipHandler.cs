using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class SpaceshipHandler : MonoBehaviour
{
	[SerializeField] float MovementSpeed = 5;

	// TODO: Convert this to an array/list of the currently held shooters
	private List<IShooter> _shooters;
	private IShooter _activeShooter;
	private Camera _mainCamera;
	private Rect _halfSpriteRect;
	private int gunLevel;
	private int damageLevel;
	private int speedLevel;
	private int laserLevel;


	private int movementX;
	private int movementY;

	void Start()
	{

		gunLevel = 0;
		damageLevel = 1;
		speedLevel = 1;
		laserLevel = 1;

		_shooters = new List<IShooter>();
		_shooters.Add(new SimpleShooter(transform.parent.gameObject));
		_shooters.Add(new ShotgunShooter(3, transform.parent.gameObject));
		_shooters.Add(new BlastShooter(gameObject));// Use the ship instead of the parent in this case because the blast should follow the ship
		_shooters.Add(new MachineGunShooter(transform.parent.gameObject));
		_activeShooter = _shooters[0];

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

		
		_activeShooter.FromCurrentShootingState(Input.GetKeyDown(shoot), Input.GetKeyUp(shoot), Input.GetKey(shoot), gameObject,Time.deltaTime,gunLevel);
	}


	public void AddShotgunPowerUp()
	{
		_activeShooter = GetShooterOfType<ShotgunShooter>();
		gunLevel = Mathf.Clamp(gunLevel+1,1,3);
	}
	public void AddMoveSpeedPowerUp()
	{
		speedLevel = Mathf.Clamp(speedLevel + 1, 1, 3);
	}
	public void AddMachineGunPowerUp()
	{
		_activeShooter = GetShooterOfType<MachineGunShooter>();
		gunLevel = Mathf.Clamp(gunLevel + 1, 1, 3);
	}
	public void AddDamagePowerUp()
	{
		damageLevel = Mathf.Clamp(damageLevel + 1, 1, 3);
	}
	public void AddLazerPowerUp()
	{
		laserLevel = Mathf.Clamp(laserLevel + 1, 1, 3);
	}

	private T GetShooterOfType<T>()
	{
		foreach (IShooter shooter in _shooters)
		{
			if (shooter is T)
				return (T)shooter;
		}
		return default(T);
	}

}
