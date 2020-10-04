using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyDamageable
{
	[SerializeField] int piecesOnDestroyed = 0;
	[SerializeField] float movementSpeed = 2;
	public GameObject[] AsteroidPieces;

	private Transform playerParentTransform;
	private Animator animator;

	// Start is called before the first frame update
	void Start()
    {
		base.Start();

		playerParentTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		base.Update();
    }

	private void FixedUpdate()
	{
		transform.Translate(Vector2.left * movementSpeed * Time.fixedDeltaTime, Space.Self);
	}

	protected override void OnZeroHealth()
	{
		if (piecesOnDestroyed <= 0)
		{
			Destroy(gameObject);
		}
		else
		{
			for (int i = 0; i < piecesOnDestroyed; i++)
			{
				var go = Instantiate(AsteroidPieces[Random.Range(0, AsteroidPieces.Length)], transform.position, Quaternion.identity, playerParentTransform);
				go.transform.Rotate(Vector3.forward, Random.Range(0f, 360f));
				Destroy(go, 10f); // Destroy them after some time so they don't keep flying
			}
			animator.Play("AsteroidExplosion");
			Destroy(GetComponent<Rigidbody2D>());
			Destroy(gameObject, 0.15f);
		}
	}
}
