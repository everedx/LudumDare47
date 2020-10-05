using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : MonoBehaviour, IInteractable
{
	public void Interact(GameObject agent)
	{
		agent.GetComponent<SpaceshipHandler>().AddDamagePowerUp();
		Destroy(gameObject);
	}

	void Start()
	{
		// WHY TF DOES INSTANTIATE(...PARENT) NOT WORK
		transform.parent = GameObject.FindGameObjectWithTag("Player").transform.parent;
	}
}
