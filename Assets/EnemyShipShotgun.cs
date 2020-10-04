using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipShotgun : EnemyDamageable
{
	[SerializeField] float speed;
	[SerializeField] float shootingFrequency;
	[SerializeField] float chanceOfSpawningBehind;

	private ShotgunShooter shotgunShooter;
	private float lastShotAt;

	// Start is called before the first frame update
	void Start()
	{
		base.Start();
		var player = GameObject.FindGameObjectWithTag("Player");
		var transformParent = player.transform.parent;

		if (Random.Range(0f, 1f) < chanceOfSpawningBehind)
		{
			transform.up = transformParent.up;
			transform.Translate(0, -40, 0, Space.Self);
			Destroy(gameObject, 10); // Destroy after some time as we just have an object destructor behind the player
		}
		else
			transform.up = -transformParent.up;

		transform.parent = transformParent;
		shotgunShooter = new ShotgunShooter(3, transform.parent.gameObject);
		lastShotAt = Time.time;
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
			shotgunShooter.FromCurrentShootingState(true, false, false, gameObject, Time.fixedDeltaTime, 1);
		}
		transform.Translate(0, speed * Time.fixedDeltaTime, 0, Space.Self);
	}
}
