using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShooter : IShooter
{
	private const string BulletPrefabPath = "Prefabs/Bullets/SimpleBullet";
	private GameObject Prefab;
	private GameObject parentObject;

	public SimpleShooter(GameObject parentObject)
	{
		Prefab = Resources.Load(BulletPrefabPath, typeof(GameObject)) as GameObject;
		this.parentObject = parentObject;
	}

	public void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip, float deltaTime, int level)
	{
		if (justPressed)
		{
			var go = Object.Instantiate(Prefab, spaceShip.transform.position, new Quaternion(), parentObject.transform);
			go.GetComponent<IBullet>().SetOwnerTag(spaceShip);
			go.transform.rotation = spaceShip.transform.rotation;
			Object.Destroy(go, 2f);
		}
	}
}
