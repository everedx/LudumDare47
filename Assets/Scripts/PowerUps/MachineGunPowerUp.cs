using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunPowerUp : MonoBehaviour, IInteractable
{


	public void Interact(GameObject agent)
	{
		agent.GetComponent<SpaceshipHandler>().AddMachineGunPowerUp();
		Destroy(gameObject);
	}


}
