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

	public void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip, float deltaTime, int level, float duration)
	{
		timer += deltaTime;

		if (isPressed && timer >= candence)
		{
			GenerateBullets(level,spaceShip);
			timer = 0;
		}
			
		
	}


	private void GenerateBullets(int level, GameObject spaceShip)
	{
		switch(level)
		{
			case 1:
				GenerateBullet(spaceShip, spaceShip.transform.position);
				break;
			case 2:
				GenerateBullet(spaceShip, Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(spaceShip.transform.position) + Vector3.up * spaceShip.transform.localScale.y * 7));
				GenerateBullet(spaceShip,Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(spaceShip.transform.position) + Vector3.down * spaceShip.transform.localScale.y * 7));
				break;
			case 3:
				GenerateBullet(spaceShip, Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(spaceShip.transform.position) + Vector3.up * spaceShip.transform.localScale.y * 7));
				GenerateBullet(spaceShip, Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(spaceShip.transform.position) + Vector3.down * spaceShip.transform.localScale.y * 7));
				GenerateBullet(spaceShip, Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(spaceShip.transform.position) + Vector3.up * spaceShip.transform.localScale.y * 20 ));
				GenerateBullet(spaceShip, Camera.main.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(spaceShip.transform.position) + Vector3.down * spaceShip.transform.localScale.y * 20));
				break;

				
		}

		
	}

	private void GenerateBullet(GameObject spaceShip,Vector3 origin)
	{
		var go = Object.Instantiate(Prefab, origin, new Quaternion(), parentObject.transform);
		go.GetComponent<IBullet>().SetOwnerTag(spaceShip);
		go.transform.rotation = spaceShip.transform.rotation;
		Object.Destroy(go, 2f);
	}

}
