using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipStraight : EnemyDamageable
{
	[SerializeField] float speed;
	[SerializeField] float shootingFrequency;

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
			shooter.FromCurrentShootingState(true, false, true, gameObject, Time.fixedDeltaTime, 1);
		}

		// Move forwards
		transform.Translate(0, speed * Time.fixedDeltaTime, 0, Space.Self);

		// Trying to chase the player
		var angle = Vector2.SignedAngle(transform.up, playerTrans.position - transform.position);
		if (Mathf.Abs(angle) > 5)
		{
			if (angle > 0)
				transform.Translate(-(speed / 4) * Time.fixedDeltaTime, 0, 0, Space.Self);
			else
				transform.Translate((speed / 4) * Time.fixedDeltaTime, 0, 0, Space.Self);
		}
	}
}
