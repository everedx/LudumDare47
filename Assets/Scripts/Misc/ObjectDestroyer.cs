using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] float timeIterations = 0.5f;
    [SerializeField] int maxNumberOfIterations = 3;
    private bool setEverythingOnfire;
    private float timer;
    private int numberOfIterations;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    private void Start()
    {
        timer = 0;
        numberOfIterations = 0;
    }

    private void Update()
    {

        if (setEverythingOnfire)
        {
            timer += Time.deltaTime;
            if (timer >= timeIterations)
            {
                List<EnemyDamageable> damageables = GameObject.FindObjectsOfType<EnemyDamageable>().ToList();
                foreach (IDamageable damageable in damageables)
                {
                    damageable.Damage(20000);
                }
                
                numberOfIterations++;
                timer = 0;
                if (numberOfIterations >= maxNumberOfIterations)
                {
                    DestroyAllPowerUps();
                    setEverythingOnfire = false;
                }
                    
            }
        }
    }

    public void DestroyScene()
    {
        timer = 0;
        numberOfIterations = 0;
        setEverythingOnfire = true;
    }

    private void DestroyAllPowerUps()
    {
        List<GameObject> powerUps = GameObject.FindGameObjectsWithTag("PowerUp").ToList();
        for (int i = 0; i < powerUps.Count; i++)
            Destroy(powerUps[i]);
    }
}
