using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : EnemyDamageable, ILevelable
{
	[SerializeField] float slowMaxSpeed;
	[SerializeField] float fastMaxSpeed;

	private enum States
	{
		EnteringTheMothafuckaScreen,
		TauntingU,
		ShootinShitInBurst,
		BoomBigLaser,
		ShotgunCraze,
		MachineTatata,
		GoBackHome
	}

	private Vector3 neutralPosition;

	private States state;
	private bool readyToChangeState;
	private float lastStateChangeAt;

	private float currentSpeed;
	private float aceleration = 5f;
	private float initialZ;

	private Vector3? tauntingPosition;
	private int maxTaunts = 3;
	private int currTaunts = 0;

	// Start is called before the first frame update
	void Start()
	{
		base.Start();
		var player = GameObject.FindGameObjectWithTag("Player");
		var transformParent = player.transform.parent;
		transform.parent = transformParent;
		transform.right = transformParent.up;

		ChangeStateTo(States.EnteringTheMothafuckaScreen);

		// Place the neutral position at half screen height and 5/6ths screen width
		neutralPosition = new Vector3((Camera.main.pixelWidth / 6) * 5, Camera.main.pixelHeight / 2);
		initialZ = transform.position.z;
	}

	private void ChangeStateTo(States newState)
	{
		state = newState;
		readyToChangeState = false;
		lastStateChangeAt = Time.time;
	}

	private void Update()
	{
		base.Update();
	}

	private void FixedUpdate()
	{
		switch (state)
		{
			case States.EnteringTheMothafuckaScreen:
				HandleEnteringTheMothafuckaScreen();
				break;
			case States.TauntingU:
				HandleTauntingU();
				break;
			case States.ShootinShitInBurst:
				HandleShootinShitInBurst();
				break;
			case States.BoomBigLaser:
				HandleBoomBigLaser();
				break;
			case States.ShotgunCraze:
				HandleShotgunCraze();
				break;
			case States.MachineTatata:
				HandleMachineTatata();
				break;
			default:
				break;
		}
	}

	private void HandleMachineTatata()
	{

	}

	private void HandleShotgunCraze()
	{

	}

	private void HandleBoomBigLaser()
	{

	}

	private void HandleShootinShitInBurst()
	{

	}

	private void HandleTauntingU()
	{
		if (tauntingPosition == null)
		{
			if (currTaunts < maxTaunts)
			{
				currTaunts++;
				Debug.Log(currTaunts);
				tauntingPosition = new Vector3(
					neutralPosition.x + Random.Range(-Camera.main.pixelWidth / 10, Camera.main.pixelWidth / 10),
					neutralPosition.y + Random.Range(-Camera.main.pixelHeight / 4, Camera.main.pixelHeight / 4),
					0);
			}
			else
			{
				currTaunts = 0;
				tauntingPosition = null;
				ChangeStateTo(States.EnteringTheMothafuckaScreen);
			}
		}

		if (MoveTowards(tauntingPosition.Value, false)) tauntingPosition = null;
	}

	private void HandleEnteringTheMothafuckaScreen()
	{
		if (MoveTowards(neutralPosition, true))
			ChangeStateTo(RandomStatusExceptNeutral());
	}

	private States RandomStatusExceptNeutral()
	{
		return States.ShootinShitInBurst;
		//return (States)Random.Range(1, 5);
	}

	private bool MoveTowards(Vector3 position, bool slowly)
	{
		var worldPosition = Camera.main.ScreenToWorldPoint(position);
		var direction = worldPosition - transform.position;
		direction = new Vector3(direction.x, direction.y, initialZ);

		// Close enough
		if (direction.magnitude < 0.5)
		{
			currentSpeed = 0;
			return true;
		}

		if (slowly && currentSpeed < slowMaxSpeed)
			currentSpeed += aceleration * Time.fixedDeltaTime;
		if (!slowly && currentSpeed < fastMaxSpeed)
			currentSpeed += aceleration * Time.fixedDeltaTime;

		var normalizedDir = direction.normalized;
		transform.Translate(normalizedDir * currentSpeed * Time.fixedDeltaTime, Space.World);

		return false;
	}

	public void SetLevel(int level)
	{
		// TODO
	}
}
