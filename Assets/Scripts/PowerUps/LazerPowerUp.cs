using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPowerUp : MonoBehaviour, IInteractable
{
	public void Interact(GameObject agent)
	{
		agent.GetComponent<SpaceshipHandler>().AddLazerPowerUp();
		Destroy(gameObject);
	}
}
