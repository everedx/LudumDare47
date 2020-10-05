using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovSpeedPowerUp : MonoBehaviour,IInteractable
{
	public void Interact(GameObject agent)
	{
		agent.GetComponent<SpaceshipHandler>().AddMoveSpeedPowerUp();
		Destroy(gameObject);
	}
}
