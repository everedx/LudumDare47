using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UIElements;

public class SpaceshipHandler : MonoBehaviour
{
	[SerializeField] float MovementSpeed = 5;
	[SerializeField] float lazerDuration = 3;

	// TODO: Convert this to an array/list of the currently held shooters
	private List<IShooter> _shooters;
	private IShooter _activeShooter;
	private Camera _mainCamera;
	private Rect _halfSpriteRect;
	private int gunLevel;
	private int damageLevel;
	private int speedLevel;
	private int laserQuantity;
	private Animator anim;
	private int lazerMax;
	private float currentSpeed;

	

	private int movementX;
	private int movementY;

	float timerLazer;


	void Start()
	{
		timerLazer = 1000;
		anim = GetComponent<Animator>();
		currentSpeed = MovementSpeed;
		lazerMax = 3;
		gunLevel = 0;
		damageLevel = 1;
		speedLevel = 1;
		laserQuantity = 0;

		_shooters = new List<IShooter>();
		_shooters.Add(new SimpleShooter(transform.parent.gameObject));
		_shooters.Add(new ShotgunShooter(transform.parent.gameObject));
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

		transform.Translate(movementX * currentSpeed * Time.fixedDeltaTime, movementY * currentSpeed * Time.fixedDeltaTime, 0, Space.Self);

	}

	void Update()
	{
		var escape = KeyCode.Escape;
		var up = KeyCode.UpArrow;
		var down = KeyCode.DownArrow;
		var left = KeyCode.LeftArrow;
		var right = KeyCode.RightArrow;
		var space = KeyCode.Space;
		var control = KeyCode.LeftControl;
		var wKey = KeyCode.W;
		var aKey = KeyCode.A;
		var sKey = KeyCode.S;
		var dKey = KeyCode.D;

		timerLazer += Time.deltaTime;



		if (Input.GetKeyDown(escape) && Time.timeScale == 1)
		{
			Time.timeScale = 0;
			Camera.main.GetComponent<MusicManager>().PauseMusic();
		}
		else if (Input.GetKeyDown(escape) && Time.timeScale == 0)
		{
			Time.timeScale = 1;
			Camera.main.GetComponent<MusicManager>().ResumeMusic();
		} 




		if (Input.GetKey(up) || Input.GetKey(wKey)) movementX = -1;
		else if (Input.GetKey(down) || Input.GetKey(sKey)) movementX = 1;
		else movementX = 0;

		if (Input.GetKey(left) || Input.GetKey(aKey)) movementY = -1;
		else if (Input.GetKey(right) || Input.GetKey(dKey)) movementY = 1;
		else movementY = 0;

		var screenPos = _mainCamera.WorldToScreenPoint(transform.position);

		// Pressing Right creates movement in the positive Y coordinates... makes these conditions a bit weird, fix it somehow if it becomes too annoying
		if (screenPos.x - _halfSpriteRect.width < 0 && movementY < 0) movementY = 0;
		else if (screenPos.x + _halfSpriteRect.width > _mainCamera.pixelWidth && movementY > 0) movementY = 0;
		if (screenPos.y - _halfSpriteRect.height < 0 && movementX > 0) movementX = 0;
		else if (screenPos.y + _halfSpriteRect.height > _mainCamera.pixelHeight && movementX < 0) movementX = 0;


			

		//Animation
		if (movementX > 0)
			anim.SetBool("GoingRight", true);
		else
			anim.SetBool("GoingRight", false);
		if (movementX < 0)
			anim.SetBool("GoingLeft", true);
		else
			anim.SetBool("GoingLeft", false);

		if (timerLazer < lazerDuration)
		{
			currentSpeed = MovementSpeed / 3;
			Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyLayer"), true);
		}
		else
		{
			Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyLayer"), false);
			currentSpeed = MovementSpeed;


			//Primary attack
			_activeShooter.FromCurrentShootingState(Input.GetKeyDown(space) || Input.GetMouseButtonDown(0), Input.GetKeyUp(space) || Input.GetMouseButtonUp(0), Input.GetKey(space) || Input.GetMouseButton(0), gameObject, Time.deltaTime, gunLevel, 2f);

			//secondary attack
			if ((Input.GetKeyDown(control) || Input.GetMouseButtonDown(1)) && laserQuantity >0)
			{
				timerLazer = 0;
				laserQuantity--;
				GetShooterOfType<BlastShooter>().FromCurrentShootingState(Input.GetKeyDown(control) || Input.GetMouseButtonDown(1), Input.GetKeyUp(control) || Input.GetMouseButtonUp(1), Input.GetKey(control) || Input.GetMouseButton(1), gameObject, Time.deltaTime, laserQuantity, lazerDuration);
			}
			
		}

		
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
		laserQuantity = Mathf.Clamp(laserQuantity + 1, 0, lazerMax);
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


	public void LoseOneLevel()
	{
		if (gunLevel == 0)
			gunLevel = 0;
		else
			gunLevel = Mathf.Clamp(gunLevel - 1, 1, 3);
		damageLevel = Mathf.Clamp(damageLevel - 1, 1, 3);
		speedLevel = Mathf.Clamp(speedLevel - 1, 1, 3);

	}


	public bool IsShootingLazer()
	{
		return timerLazer < lazerDuration;
	}

}
