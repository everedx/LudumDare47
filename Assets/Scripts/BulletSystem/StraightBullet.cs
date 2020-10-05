using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour, IBullet
{
	[SerializeField] float damage = 1.1f;
	[SerializeField] float speed = 5f;

	private const string ShotPath = "Audios/Sfx - Wall hit";
	private static AudioClip shotClip = null;

	float damageMultiplier = 1;

	private string ownerTag;
	// Start is called before the first frame update
	void Start()
	{
		if(shotClip == null)
			shotClip = Resources.Load(ShotPath, typeof(AudioClip)) as AudioClip;
		AudioSource.PlayClipAtPoint(shotClip, transform.position);
	}

	void FixedUpdate()
	{
		transform.Translate(0, speed * Time.fixedDeltaTime, 0, Space.Self);
	}

	public float HasHitSomething()
	{
		
		Destroy(gameObject);

		return damage * damageMultiplier; // Damage this bullet does
	}

	public void SetOwnerTag(GameObject go)
	{
		// Keep owner tag instead of owner in order to be able to be hit by destroyed gameobjects
		ownerTag = go.tag;
	}

	public string GetOwnerTag()
	{
		return ownerTag;
	}

	public void SetDamageMultiplier(float multiplier)
	{
		damageMultiplier = multiplier;
	}
}
