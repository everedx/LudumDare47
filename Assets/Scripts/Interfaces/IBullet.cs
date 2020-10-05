using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
	float HasHitSomething();
	string GetOwnerTag();
	void SetOwnerTag(GameObject go);
	void SetDamageMultiplier(float multiplier);
}
