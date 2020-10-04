using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour, IBullet
{
	[SerializeField] float damage = 1.1f;
	[SerializeField] float speed = 5f;
	[SerializeField] float growthSpeed = 10f;
	[SerializeField] float headSpeed = 40f;
	[SerializeField] float headStopMargin = 1f;

	private string ownerTag;
	private SpriteRenderer _bodyRenderer;
	private GameObject _head;
	private float _raycastDistance;
	private RaycastHit2D[] results = new RaycastHit2D[10];
	private bool keepMovingHead = true;
	private float headScreenOverlap = 10;

	// Start is called before the first frame update
	void Start()
	{
		_bodyRenderer = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
		_head = transform.GetChild(0).gameObject;

		// Calculate distance in world units for the length of the raycast
		_raycastDistance = 100f;
	}

	private void Update()
	{
		if (ownerTag == "Player" && Camera.main.WorldToScreenPoint(_head.transform.position).x < Camera.main.pixelWidth + headScreenOverlap ||
			ownerTag != "Player" && Camera.main.WorldToScreenPoint(_head.transform.position).x > - headScreenOverlap)
		{
			_bodyRenderer.size = new Vector2(_bodyRenderer.size.x, _bodyRenderer.size.y + growthSpeed * Time.deltaTime);
			if (keepMovingHead) _head.transform.Translate(0, headSpeed * Time.deltaTime, 0, Space.Self); // Avoid jittery head
			else AnchorHeadToScreenPosition();
		}
		else
		{
			AnchorHeadToScreenPosition();
		}
	}

	private void AnchorHeadToScreenPosition()
	{
		keepMovingHead = false;
		var blastScreenPos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 pos;
		if(ownerTag == "Player")
			pos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth + headScreenOverlap, blastScreenPos.y, 0));
		else
			pos = Camera.main.ScreenToWorldPoint(new Vector3(-headScreenOverlap, blastScreenPos.y, 0));

		var finalPos = new Vector3(pos.x, pos.y, _head.transform.position.z);
		_head.transform.position = finalPos;
	}

	void FixedUpdate()
	{
		ContactFilter2D cf = new ContactFilter2D();
		cf.useTriggers = true;

		var count = Physics2D.Raycast(transform.position, transform.up, cf, results, _raycastDistance);

		for(int i = 0; i < count; i++)
		{
			var hit = results[i];
			var damageable = hit.collider.gameObject.GetComponent<IDamageable>();
			if (damageable != null && hit.collider.tag != ownerTag)
			{
				damageable.Damage(damage);
			}
		}
	}

	// Leave empty as we are not going to use colliders for this
	public float HasHitSomething() { return 0; }

	public void SetOwnerTag(GameObject go)
	{
		// Keep owner tag instead of owner in order to be able to be hit by destroyed gameobjects
		ownerTag = go.tag;
	}

	public string GetOwnerTag()
	{
		return ownerTag;
	}
}