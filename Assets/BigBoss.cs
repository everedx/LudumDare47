using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : EnemyDamageable, ILevelable
{
	[SerializeField] float slowMaxSpeed;
	[SerializeField] float fastMaxSpeed;
	[SerializeField] float machinegunFreq;

	private enum States
	{
		EnteringTheMothafuckaScreen,
		TauntingU,
		BoomBigLaser,
		ShotgunCraze,
		MachineTatata
	}

	private Vector3 neutralPosition;

	private States state;
	private bool readyToChangeState;
	private float lastStateChangeAt;

	private float currentSpeed;
	private float aceleration = 5f;
	private float initialZ;

	private Vector3? movingToPosition;

	private int maxTaunts = 3;
	private int maxShotguns = 3;
	private int maxMachineguns = 3;
	private int maxBlasts = 1;

	private int currMovements = 0;
	private ShotgunShooter shotgun;
	private MachineGunShooter machinegunLeft;
	private MachineGunShooter machinegunRight;
	private BlastShooter blast;
	private float lastMachinegunShot;

	private Transform center;
	private Transform left;
	private Transform right;

	// Start is called before the first frame update
	void Start()
	{
		base.Start();
		var player = GameObject.FindGameObjectWithTag("Player");
		var transformParent = player.transform.parent;
		transform.parent = transformParent;
		transform.up = -transformParent.up;

		ChangeStateTo(States.EnteringTheMothafuckaScreen);

		// Place the neutral position at half screen height and 5/6ths screen width
		neutralPosition = new Vector3((Camera.main.pixelWidth / 6) * 5, Camera.main.pixelHeight / 2);
		initialZ = transform.position.z;

		for (int i = 0; i < transform.childCount; i++)
		{
			var child = transform.GetChild(i);
			if (child.name == "Center") center = child;
			if (child.name == "Left") left = child;
			if (child.name == "Right") right = child;
		}

		shotgun = new ShotgunShooter(center.gameObject);
		machinegunLeft = new MachineGunShooter(left.gameObject);
		machinegunRight = new MachineGunShooter(right.gameObject);
		blast = new BlastShooter(center.gameObject);
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
		if (movingToPosition == null)
		{
			if (currMovements < maxMachineguns)
			{
				currMovements++;
				Debug.Log(currMovements);
				movingToPosition = new Vector3(
					neutralPosition.x + Random.Range(-Camera.main.pixelWidth / 10, Camera.main.pixelWidth / 10),
					neutralPosition.y + Random.Range(-Camera.main.pixelHeight / 3, Camera.main.pixelHeight / 3),
					0);
			}
			else
			{
				currMovements = 0;
				movingToPosition = null;
				ChangeStateTo(States.EnteringTheMothafuckaScreen);
			}
		}

		if (MoveTowards(movingToPosition.Value, false))
		{
			movingToPosition = null;
		}

		if(Time.time - machinegunFreq > lastMachinegunShot)
		{
			machinegunLeft.FromCurrentShootingState(false, false, true, left.gameObject, 666, 1, 0f);
			machinegunRight.FromCurrentShootingState(false, false, true, right.gameObject, 666, 1, 0f);
			lastMachinegunShot = Time.time;
		}
	}

	private void HandleShotgunCraze()
	{
		if (movingToPosition == null)
		{
			if (currMovements < maxShotguns)
			{
				currMovements++;
				Debug.Log(currMovements);
				movingToPosition = new Vector3(
					neutralPosition.x + Random.Range(-Camera.main.pixelWidth / 10, Camera.main.pixelWidth / 10),
					neutralPosition.y + Random.Range(-Camera.main.pixelHeight / 3, Camera.main.pixelHeight / 3),
					0);
			}
			else
			{
				currMovements = 0;
				movingToPosition = null;
				ChangeStateTo(States.EnteringTheMothafuckaScreen);
			}
		}

		if (MoveTowards(movingToPosition.Value, false))
		{
			shotgun.FromCurrentShootingState(true, false, false, center.gameObject, Time.fixedDeltaTime, 1, 1f);
			movingToPosition = null;
		}
	}

	private void HandleBoomBigLaser()
	{
		if (movingToPosition == null)
		{
			if (currMovements < maxBlasts)
			{
				currMovements++;
				Debug.Log(currMovements);
				movingToPosition = new Vector3(
					neutralPosition.x + Random.Range(-Camera.main.pixelWidth / 10, Camera.main.pixelWidth / 10),
					neutralPosition.y + Random.Range(-Camera.main.pixelHeight / 3, Camera.main.pixelHeight / 3),
					0);
			}
			else
			{
				currMovements = 0;
				movingToPosition = null;
				ChangeStateTo(States.EnteringTheMothafuckaScreen);
			}
		}

		if (MoveTowards(movingToPosition.Value, false))
		{
			movingToPosition = null;
		}

		blast.FromCurrentShootingState(true, false, false, center.gameObject, Time.fixedDeltaTime, 1, 1);
	}

	private void HandleTauntingU()
	{
		if (movingToPosition == null)
		{
			if (currMovements < maxTaunts)
			{
				currMovements++;
				Debug.Log(currMovements);
				movingToPosition = new Vector3(
					neutralPosition.x + Random.Range(-Camera.main.pixelWidth / 10, Camera.main.pixelWidth / 10),
					neutralPosition.y + Random.Range(-Camera.main.pixelHeight / 4, Camera.main.pixelHeight / 4),
					0);
			}
			else
			{
				currMovements = 0;
				movingToPosition = null;
				ChangeStateTo(States.EnteringTheMothafuckaScreen);
			}
		}

		if (MoveTowards(movingToPosition.Value, false)) movingToPosition = null;
	}

	private void HandleEnteringTheMothafuckaScreen()
	{
		if (MoveTowards(neutralPosition, true))
			ChangeStateTo(RandomStatusExceptNeutral());
	}

	private States RandomStatusExceptNeutral()
	{
		return States.BoomBigLaser;
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
