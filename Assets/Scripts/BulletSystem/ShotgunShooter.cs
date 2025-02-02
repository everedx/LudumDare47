﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooter : IShooter
{
	private const string BulletPrefabPath = "Prefabs/Bullets/ShotGunBullet";
	private GameObject Prefab;
	private GameObject parentObject;

	public ShotgunShooter(GameObject parentObject)
	{
		Prefab = Resources.Load(BulletPrefabPath, typeof(GameObject)) as GameObject;
		this.parentObject = parentObject;
	}

	public void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip, float deltaTime, int level, float duration, float damageMultiplier = 1)
	{
		if (justPressed)
		{
			float eulerDif = 100f / (2*(level));
			for (float i = -50 ; i <= 50; i += eulerDif)
			{
				var go = Object.Instantiate(Prefab, spaceShip.transform.position, spaceShip.transform.rotation, parentObject.transform);
				go.GetComponent<IBullet>().SetOwnerTag(spaceShip);
				go.GetComponent<IBullet>().SetDamageMultiplier(damageMultiplier);
				go.transform.Rotate(0, 0, i);
				Object.Destroy(go, 2f);
			}
		}
	}
}
