using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePopup : MonoBehaviour
{
	[SerializeField] float maxSpeed;
	[SerializeField] float aceleration;
	[SerializeField] float timeToWait;

	bool goingUp;
	bool bouncingDown;
	bool goingDown;
	bool waiting;

	RectTransform image;
	float speed = 0;
	float startedWaitingAt;

	// Start is called before the first frame update
	void Start()
    {
		image = transform.GetChild(0).GetComponent<RectTransform>();
		image.position = new Vector2(image.position.x, -25);
		goingUp = true;
		speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
		if (goingUp)
		{
			image.position = new Vector2(image.position.x, image.position.y + speed * Time.deltaTime);
			if (image.position.y > Camera.main.pixelHeight / 2)
			{
				goingUp = false;
				bouncingDown = true;
			}
		}
		if (bouncingDown)
		{
			speed -= aceleration * Time.deltaTime;
			image.position = new Vector2(image.position.x, image.position.y + speed * Time.deltaTime);
			if (image.position.y < Camera.main.pixelHeight / 2)
			{
				bouncingDown = false;
				waiting = true;
				startedWaitingAt = Time.time;
			}
		}
		if (waiting)
		{
			if (Time.time - startedWaitingAt > timeToWait)
			{
				waiting = false;
				goingDown = true;
				speed = maxSpeed;
				Destroy(gameObject, 2);
			}
		}
		if (goingDown)
		{
			speed -= aceleration * Time.deltaTime;
			image.position = new Vector2(image.position.x, image.position.y + speed * Time.deltaTime);
		}
	}
}
