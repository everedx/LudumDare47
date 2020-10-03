using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooter : IShooter
{
	private const string BulletPrefabPath = "Prefabs/Bullets/SimpleBullet";
	private GameObject Prefab;
	private GameObject parentObject;
	private int numberOfBullets;

	public ShotgunShooter(int numberOfBullets, GameObject parentObject)
	{
		this.numberOfBullets = numberOfBullets;
		Prefab = Resources.Load(BulletPrefabPath, typeof(GameObject)) as GameObject;
		this.parentObject = parentObject;
	}

	public void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip)
	{
		if (justPressed)
		{
			float eulerDif = 100f / numberOfBullets;
			for (float i = -50; i <= 50; i += eulerDif)
			{
				var go = Object.Instantiate(Prefab, spaceShip.transform.position, spaceShip.transform.rotation, parentObject.transform);
				go.GetComponent<StraightBullet>().SetOwner(spaceShip);
				go.transform.Rotate(0, 0, i);
				Object.Destroy(go, 2f);
			}
		}
	}
}
