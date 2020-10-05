using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
	public GameObject Damage;
	public GameObject Laser;
	public GameObject Machinegun;
	public GameObject Shield;
	public GameObject Shotgun;
	public GameObject Speed;

	public static MessageManager Instance;

	private void Start()
	{
		Instance = this;
	}

	public void ShowDamage()
	{
		Instantiate(Damage);
	}

	public void ShowLaser()
	{
		Instantiate(Laser);
	}

	public void ShowMachinegun()
	{
		Instantiate(Machinegun);
	}

	public void ShowShield()
	{
		Instantiate(Shield);
	}

	public void ShowShotgun()
	{
		Instantiate(Shotgun);
	}

	public void ShowSpeed()
	{
		Instantiate(Speed);
	}
}
