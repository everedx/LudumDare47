using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructableObject : MonoBehaviour
{
	public float DestroyInMillis;

	// Start is called before the first frame update
	void Start()
	{
		Destroy(this, DestroyInMillis);
	}
}
