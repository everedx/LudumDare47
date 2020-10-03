using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D coll)
	{
		var interactable = coll.gameObject.GetComponent<IInteractable>();
		if (interactable != null)
		{
			interactable.Interact(gameObject);
		}
	}
}
