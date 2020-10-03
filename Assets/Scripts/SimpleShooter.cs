using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShooter : IShooter
{
	private const string BulletPrefabPath = "Prefabs/Bullets/SimpleBullet";
	private GameObject Prefab;

	public SimpleShooter()
	{
		Prefab = Resources.Load(BulletPrefabPath, typeof(GameObject)) as GameObject;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip)
	{
		if (justPressed)
		{
			var go = Object.Instantiate(Prefab, spaceShip.transform.position, new Quaternion());
			go.transform.rotation = spaceShip.transform.rotation;
			Object.Destroy(go, 2f);
		}
	}
}
