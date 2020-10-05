using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooter
{
	void FromCurrentShootingState(bool justPressed, bool justReleased, bool isPressed, GameObject spaceShip, float deltaTime, int level, float duration);
}
