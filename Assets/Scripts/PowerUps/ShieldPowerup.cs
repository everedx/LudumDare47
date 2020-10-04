using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour, IInteractable
{
	public float shieldHealth;

	private const string ShieldPrefabPath = "Prefabs/Shield";
	private GameObject _shield;

	public void Interact(GameObject agent)
	{
		agent.GetComponent<PlayerHealth>().AddShield(_shield, this);

		Destroy(gameObject);
	}

	// Start is called before the first frame update
	void Start()
    {
		_shield = Resources.Load(ShieldPrefabPath, typeof(GameObject)) as GameObject;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
