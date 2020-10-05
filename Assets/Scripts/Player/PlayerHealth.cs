using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float initialHealth;
    [SerializeField] private float invulTimeAfterHit;
    [SerializeField] private float flickerDamageAmount;
    [SerializeField] private float glitchDamageAmount;
    [SerializeField] private float flickerDamageAmountEnd;
    [SerializeField] private float glitchDamageAmountEnd;
    [SerializeField] private float maxShieldHealth = 3f;

	public GameObject HealthRing;
    public GameObject ShieldRing;


    private float currentHealth;
    private float invulnerabilityTimer;
    private Material playerMaterial;
	private Image healthRing;
    private Image shieldRing;
    private float currentDisplayedHealth; // Not the actual health, only used to display smoothed health changes
	private float currentShieldHealth;
	private GameObject _shield;
    private SpaceshipHandler ship;

	// Start is called before the first frame update
	void Start()
    {
        currentHealth = initialHealth;
        playerMaterial = GetComponent<SpriteRenderer>().material;
        invulnerabilityTimer = 50000;
        ship = GetComponent<SpaceshipHandler>();
		healthRing = HealthRing.GetComponent<Image>();
        shieldRing = ShieldRing.GetComponent<Image>();
    }

    private void Update()
    {
        invulnerabilityTimer += Time.deltaTime;
        if (invulnerabilityTimer < invulTimeAfterHit)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyLayer"), true);
        }
        else
        {
            
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("EnemyLayer"), false);
            
        }

        playerMaterial.SetFloat("_GlitchAmount", Mathf.Lerp(glitchDamageAmount, glitchDamageAmountEnd, invulnerabilityTimer / invulTimeAfterHit));
        playerMaterial.SetFloat("_FlickerFreq", Mathf.Lerp(flickerDamageAmount, flickerDamageAmountEnd, invulnerabilityTimer / invulTimeAfterHit));

		healthRing.fillAmount = Mathf.Lerp(healthRing.fillAmount, currentHealth / initialHealth, 0.1f);
        shieldRing.fillAmount = Mathf.Lerp(shieldRing.fillAmount, currentShieldHealth / maxShieldHealth, 0.1f);
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        var bullet = coll.gameObject.GetComponent<IBullet>();
        if (bullet != null && !bullet.GetOwnerTag().Equals("Player"))
        {
			Damage(bullet.HasHitSomething());
        }

    }
    public void Damage(float amount)
    {
        if (invulnerabilityTimer < invulTimeAfterHit || ship.IsShootingLazer())
        {
            return;
        }

        invulnerabilityTimer = 0;

		if (currentShieldHealth > 0)
		{
			currentShieldHealth -= amount;
			if (currentShieldHealth <= 0) Destroy(_shield);
			return; // If we had shield when this damage occurred, don't ever affect health
		}

        GetComponent<SpaceshipHandler>().LoseOneLevel();


        currentHealth= Mathf.Clamp(currentHealth - amount, 0, initialHealth);
        if (currentHealth == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void AddShield(GameObject shieldPrefab, ShieldPowerup powerup)
	{
        Destroy(_shield);
        _shield = Instantiate(shieldPrefab, transform);
		currentShieldHealth = Mathf.Clamp(currentShieldHealth+ powerup.shieldHealth,0,maxShieldHealth);
	}
}
