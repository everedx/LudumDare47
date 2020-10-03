using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class BurstShip : EnemyDamageable
{
   
    [SerializeField] float secondsToShoot = 2;
    [SerializeField] float secondsToGoAfterShoot = 2;
    [SerializeField] float timeToArriveAndScape = 3;
    [SerializeField] int numberOfBullets = 24;
    private Transform playerParentTransform;
    private float timer;
    private Statutes status;
    private float distanceToPlayer;
    private float targetDistance = 10;
    private IShooter _shooter;

    private enum Statutes
    {
        GettingClose,
        Shooting,
        GoingAway
    }


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        playerParentTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        transform.parent = playerParentTransform;
        status = Statutes.GettingClose;
        timer = 0;
        distanceToPlayer = Vector2.Distance(transform.position, playerParentTransform.position);
        _shooter = new RadialShooter(24, playerParentTransform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        switch (status)
        {
            case Statutes.GettingClose:
                GotoThePlayer();
                break;
            case Statutes.Shooting:
                Shoot();
                break;
            case Statutes.GoingAway:
                Escape();
                break;
        }
    }


    private void GotoThePlayer()
    {
        float distanceToAchieve = Mathf.SmoothStep(distanceToPlayer, targetDistance, timer / timeToArriveAndScape);
        Vector2 newPosition = GetPointDistanceFromObject(distanceToAchieve, (transform.position-playerParentTransform.position  ).normalized);
        transform.position = newPosition;
        if (distanceToAchieve == targetDistance)
        {
            if (status == Statutes.GettingClose)
            {
                
                status = Statutes.Shooting;
                _shooter.FromCurrentShootingState(false,false,false,gameObject);
                timer = 0;
            }
           
        }
    }

    private void Shoot()
    {
        if (timer >= secondsToShoot)
        {
            _shooter.FromCurrentShootingState(false, false, false, gameObject);
            status = Statutes.GoingAway;
            targetDistance = distanceToPlayer;
            distanceToPlayer = Vector2.Distance(transform.position, playerParentTransform.position);
            timer = 0;

        }
    }

    private void Escape()
    {
        float distanceToAchieve = Mathf.SmoothStep(distanceToPlayer, targetDistance, timer / timeToArriveAndScape);
        Vector2 newPosition = GetPointDistanceFromObject(distanceToAchieve, (transform.position - playerParentTransform.position).normalized);
        transform.position = newPosition;
        if (distanceToAchieve == targetDistance)
        {
            Destroy(gameObject);
        }
    }

    Vector2 GetPointDistanceFromObject(float distanceFromSurface, Vector2 direction)
    {
        Vector2 finalDirection = direction.normalized * distanceFromSurface;
        Vector2 targetPosition = playerParentTransform.position + Utils.Vec2To3(finalDirection);
        return targetPosition;
    }
}
