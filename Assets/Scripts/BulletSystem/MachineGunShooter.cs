using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunShooter : IShooter
{
	private const string BulletPrefabPath = "Prefabs/Bullets/MachineGunBullet";
	private GameObject Prefab;
	private GameObject parentObject;
	private float timer;
	private float candence = 0.1f;


	public MachineGunShooter(GameObject parentObject)
	{
		Prefab = Resources.Load(BulletPrefabPath, typeof(GameObject)) as GameObject;
		this.parentObject = parentObject;
		timer = 2000;
	}

	public void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip, float deltaTime, int level)
	{
		timer += deltaTime;

		if (isPressed && timer >= candence)
		{
			var go = Object.Instantiate(Prefab, spaceShip.transform.position, new Quaternion(), parentObject.transform);
			go.GetComponent<IBullet>().SetOwnerTag(spaceShip);
			go.transform.rotation = spaceShip.transform.rotation;
			Object.Destroy(go, 2f);
			timer = 0;
		}
			
		
	}
}
