using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipShotgun : EnemyDamageable, ILevelable
{
	[SerializeField] float speed;
	[SerializeField] float shootingFrequency;
	[SerializeField] float chanceOfSpawningBehind;
	[SerializeField] int shotgunLevel;

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
		shotgunShooter = new ShotgunShooter(transform.parent.gameObject);
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
			shotgunShooter.FromCurrentShootingState(true, false, false, gameObject, Time.fixedDeltaTime, shotgunLevel, 0f);
		}
		transform.Translate(0, speed * Time.fixedDeltaTime, 0, Space.Self);
	}

	public void SetLevel(int level)
	{
		shootingFrequency = 3 - 0.1f * level;
		currentHealth = 0.5f + 0.5f * level;
		initialHealth = 0.5f + 0.5f * level;

		if (level < 4) shotgunLevel = 1;
		else if (level < 8) shotgunLevel = 2;
		else shotgunLevel = 3;
	}
}
