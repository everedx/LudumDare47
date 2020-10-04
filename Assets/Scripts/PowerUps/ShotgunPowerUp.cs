using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPowerUp : MonoBehaviour, IInteractable
{
	public void Interact(GameObject agent)
	{
		agent.GetComponent<SpaceshipHandler>().AddShotgunPowerUp();
		Destroy(gameObject);
	}
}
