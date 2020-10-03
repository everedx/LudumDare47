using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour, IBullet
{
	[SerializeField] float damage = 1.1f;
	[SerializeField] float speed = 5f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	void FixedUpdate()
	{
		transform.Translate(0, speed * Time.fixedDeltaTime, 0, Space.Self);
	}

	public float HasHitSomething()
	{
		Destroy(gameObject);

		return damage; // Damage this bullet does
	}

}
