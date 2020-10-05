using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] Shader shaderObject;
    [SerializeField] protected float initialHealth = 3;
    [SerializeField] float damageOnCollision = 1;
    [SerializeField] ParticleSystem psHit;
    [SerializeField] protected GameObject lootObject;
	[SerializeField] int score;

	private const string ExplosionPath = "Audios/Sfx - Explosion 2";
	private static AudioClip explosionClip;

	protected float currentHealth;
    protected Rigidbody2D rigBody;
    protected BoxCollider2D boxCollider2D;
    protected SpriteRenderer sr;
    private Material mat;
    protected GameObject playerObject;
    protected float initializationTime;

	private ScoreManager scoreManager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        currentHealth = initialHealth;
        rigBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.material = new Material(shaderObject);
        mat = sr.material;
		scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();

		if(explosionClip == null)
			explosionClip = Resources.Load(ExplosionPath, typeof(AudioClip)) as AudioClip;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (mat.GetFloat("_BlendMagnitude") != 1)
        {
            mat.SetFloat("_BlendMagnitude", Mathf.Clamp(mat.GetFloat("_BlendMagnitude") + Time.deltaTime * 2, 0, 1));
        }
    }

    public virtual void Damage(float damage)
    {
		if (currentHealth > 0)
        {
            currentHealth = currentHealth - damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, initialHealth);

            ReleaseParticlesFromHit();

          

            //shiny
            mat.SetFloat("_BlendMagnitude", 0.95f);

            Debug.Log("The Crawler " + gameObject.name + " received " + damage + " damage.");
            if (currentHealth == 0)
            {
				AudioSource.PlayClipAtPoint(explosionClip, transform.position);
				scoreManager.AddScore(score);
				OnZeroHealth();
                Debug.Log("The Enemy " + gameObject.name + " died.");
            }
      
        }

    }

	protected virtual void OnZeroHealth()
	{
		if (lootObject != null)
		{
			Instantiate(lootObject, transform.position, Quaternion.identity, transform.parent);
		}
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		var bullet = coll.gameObject.GetComponent<IBullet>();
        if (bullet != null && bullet.GetOwnerTag().Equals("Player"))
        {
            Damage(bullet.HasHitSomething());
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag.Equals("Player"))
        {
			collision.gameObject.GetComponent<PlayerHealth>().Damage(damageOnCollision);
        }
    }

    private void ReleaseParticlesFromHit()
    {
        if (psHit != null)
            psHit.Play();
    }

}
