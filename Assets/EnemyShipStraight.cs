using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStraight : EnemyDamageable, ILevelable
{
	[SerializeField] float speed;
	[SerializeField] float shootingFrequency;
	[SerializeField] int machinegunLevel;
	[SerializeField] float verticalSpeedDivider;

	private MachineGunShooter shooter;
	private float lastShotAt;
	private Transform playerTrans;

	// Start is called before the first frame update
	void Start()
	{
		base.Start();
		var player = GameObject.FindGameObjectWithTag("Player");
		var transformParent = player.transform.parent;

		transform.up = -transformParent.up;

		transform.parent = transformParent;
		shooter = new MachineGunShooter(transform.parent.gameObject);
		lastShotAt = Time.time;
		playerTrans = player.transform;
	}

	private void Update()
	{
		base.Update();
	}

	private void FixedUpdate()
	{
		if (Time.time - lastShotAt > shootingFrequency)
		{
			lastShotAt = Time.time;
			shooter.FromCurrentShootingState(true, false, true, gameObject, 666, machinegunLevel);
		}

		// Move forwards
		transform.Translate(0, speed * Time.fixedDeltaTime, 0, Space.Self);

		// Trying to chase the player
		var angle = Vector2.SignedAngle(transform.up, playerTrans.position - transform.position);
		if (Mathf.Abs(angle) > 5)
		{
			if (angle > 0)
				transform.Translate(-(speed / verticalSpeedDivider) * Time.fixedDeltaTime, 0, 0, Space.Self);
			else
				transform.Translate((speed / verticalSpeedDivider) * Time.fixedDeltaTime, 0, 0, Space.Self);
		}
	}

	public void SetLevel(int level)
	{
		shootingFrequency = 3 - 0.2f * level;
		currentHealth = 0.5f + 0.2f * level;
		initialHealth = 0.5f + 0.2f * level;

		if (level < 2)
		{
			verticalSpeedDivider = 10;
			machinegunLevel = 1;
		}
		else if (level < 5)
		{
			verticalSpeedDivider = 7;
			machinegunLevel = 2;
		}
		else
		{
			verticalSpeedDivider = 5;
			machinegunLevel = 3;
		}
	}
}
