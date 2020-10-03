using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	public float YOffset;
	public float XOffset;
	public GameObject ToFollow;
	Transform _transformToFollow;

	// Start is called before the first frame update
	void Start()
	{
		_transformToFollow = ToFollow.transform;
	}

	// Update is called once per frame
	void Update()
	{
		transform.rotation = _transformToFollow.rotation;
		transform.up = -transform.up;
		transform.Rotate(Vector3.back, 90, Space.Self);
		transform.position = new Vector3(_transformToFollow.position.x, _transformToFollow.position.y, transform.position.z);
		transform.Translate(new Vector3(XOffset, YOffset, 0), Space.Self);
	}
}
