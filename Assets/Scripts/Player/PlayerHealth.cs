using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float initialHealth;
    [SerializeField] private float invulTimeAfterHit;
    [SerializeField] private float flickerDamageAmount;
    [SerializeField] private float glitchDamageAmount;
    [SerializeField] private float flickerDamageAmountEnd;
    [SerializeField] private float glitchDamageAmountEnd;


    private float currentHealth;
    private float invulnerabilityTimer;
    private Material playerMaterial;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = initialHealth;
        playerMaterial = GetComponent<SpriteRenderer>().material;
        invulnerabilityTimer = 50000;
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
    }



    public void Damage(float amount)
    {
        invulnerabilityTimer = 0;
        currentHealth= Mathf.Clamp(currentHealth - amount, 0, initialHealth);
        if (currentHealth == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
