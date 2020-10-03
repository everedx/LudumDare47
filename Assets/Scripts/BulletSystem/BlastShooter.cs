using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastShooter : IShooter
{
	[SerializeField] float yOffset = 2.65f;

	private const string BulletPrefabPath = "Prefabs/Bullets/Blast";
	private GameObject Prefab;
	private GameObject parentObject;

	public BlastShooter(GameObject parentObject)
	{
		Prefab = Resources.Load(BulletPrefabPath, typeof(GameObject)) as GameObject;
		this.parentObject = parentObject;
	}

	public void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip)
	{
		if (justPressed)
		{
			var go = Object.Instantiate(Prefab, spaceShip.transform.position, new Quaternion(), parentObject.transform);
			go.GetComponent<IBullet>().SetOwnerTag(spaceShip);
			go.transform.rotation = spaceShip.transform.rotation;
			go.transform.Translate(0, yOffset, 0, Space.Self);
			Object.Destroy(go, 2f);
		}
	}
}
