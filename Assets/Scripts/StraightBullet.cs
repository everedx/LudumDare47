using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour, IBullet
{
	public float HasHitSomething()
	{
		Destroy(gameObject);

		return 1.1f; // Damage this bullet does
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	void FixedUpdate()
	{
		transform.Translate(0, 50 * Time.fixedDeltaTime, 0, Space.Self);
	}
}
