using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialShooter : IShooter
{
	private const string BulletPrefabPath = "Prefabs/Bullets/RadialBullet";

	private GameObject Prefab;
	private int numberOfBullets;
	private GameObject parentOfBullet;

	public RadialShooter(int numberOfBullets, GameObject parentObject)
	{
		Prefab = Resources.Load(BulletPrefabPath, typeof(GameObject)) as GameObject;
		this.numberOfBullets = numberOfBullets;
		parentOfBullet = parentObject;
	}

	public void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip)
	{
		float eulerDif = 360f / numberOfBullets;
		for (float i = 0; i < 360; i += eulerDif)
		{
			var go = Object.Instantiate(Prefab, spaceShip.transform.position, Quaternion.Euler(0,0,i), parentOfBullet.transform);
			go.GetComponent<StraightBullet>().SetOwnerTag(spaceShip);
			Object.Destroy(go, 7f);
		}
		
	}




}
