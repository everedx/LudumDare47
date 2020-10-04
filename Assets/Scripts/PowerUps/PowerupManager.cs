using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{


	private void OnTriggerEnter2D(Collider2D coll)
	{
		var interactable = coll.gameObject.GetComponent<IInteractable>();
		if (interactable != null)
		{
			interactable.Interact(gameObject);
		}
	}
}
