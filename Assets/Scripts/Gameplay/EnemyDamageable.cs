using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] Shader shaderObject;
    [SerializeField] float initialHealth = 3;
    [SerializeField] ParticleSystem psHit;
    protected float currentHealth;
    protected Rigidbody2D rigBody;
    protected BoxCollider2D boxCollider2D;
    protected SpriteRenderer sr;
    private Material mat;
    protected GameObject playerObject;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        currentHealth = initialHealth;
        rigBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.material = new Material(shaderObject);
        mat = sr.material;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (mat.GetFloat("_BlendMagnitude") != 1)
        {
            mat.SetFloat("_BlendMagnitude", Mathf.Clamp(mat.GetFloat("_BlendMagnitude") + Time.deltaTime * 2, 0, 1));
        }
    }

    public virtual void Damage(float damage, Vector3 originOfDamage, GameObject originObject)
    {

        if (currentHealth > 0)
        {
            currentHealth = currentHealth - damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, initialHealth);

            ReleaseParticlesFromHit();

            //shiny
            mat.SetFloat("_BlendMagnitude", 0.75f);

            Debug.Log("The Crawler " + gameObject.name + " received " + damage + " damage.");
            if (currentHealth == 0)
            {
                Destroy(gameObject,1);
                Debug.Log("The Enemy " + gameObject.name + " died.");
            }
      
        }

    }




    private void ReleaseParticlesFromHit()
    {
        if (psHit != null)
            psHit.Play();
    }

}
