using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMob : EnemyDamageable
{
    [SerializeField] float timeVulnerableInScreen = 5;
    [SerializeField] float timeToArriveAndScape = 3;
    [SerializeField] float movSpeedWandering = 3f;
    [SerializeField] float timeWaveMovement = 1;

    
    private Transform playerParentTransform;
    private float timer;
    private Statuses status;
    private float targetDistance = 10;
    private float distanceToPlayer;
    private float wanderTargetDistance;
    private Vector2 wanderTargetDirection;
    private Vector2 wanderOriginDirection;
    private float wanderOriginDistance;
    private float timerWave;
    float waveSpeed;
    private float distanceToTargetLocation;
    private enum Statuses
    {
        GettingClose,
        Wandering,
        GoingAway
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        waveSpeed = 0;
        targetDistance = Random.Range(0, 11);
        playerParentTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        transform.parent = playerParentTransform;
        status = Statuses.GettingClose;
        timer = 0;
        timerWave = 0;
        distanceToPlayer = Vector2.Distance(transform.position, playerParentTransform.position);
    }

    private void FixedUpdate()
    {
        if (status == Statuses.Wandering)
        {
            transform.Translate(waveSpeed * Time.fixedDeltaTime,0,0,Space.Self);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        switch (status)
        {
            case Statuses.GettingClose:
                GotoThePlayer();
                break;
            case Statuses.Wandering:
                Wander();
                break;
            case Statuses.GoingAway:
                Escape();
                break;
        }

    }


    private void GotoThePlayer()
    {
        float distanceToAchieve = Mathf.SmoothStep(distanceToPlayer, targetDistance, timer / timeToArriveAndScape);
        Vector2 newPosition = GetPointDistanceFromObject(distanceToAchieve, (transform.position - playerParentTransform.position).normalized, playerParentTransform.position);
        transform.position = newPosition;
        if (distanceToAchieve == targetDistance)
        {
            if (status == Statuses.GettingClose)
            {

                status = Statuses.Wandering;
                timer = 0;

            }

        }
    }

    private void Wander()
    {
        // status = Statuses.GoingAway;
        timerWave += Time.deltaTime;
        if (timer <= timeVulnerableInScreen)
        {
            //transform.Translate();
            if (timerWave >= 2*timeWaveMovement)
            {
                timerWave = 0;
                movSpeedWandering = -movSpeedWandering;
            }

            if(timerWave < timeWaveMovement)
                waveSpeed = Mathf.SmoothStep(0, movSpeedWandering, timerWave / timeWaveMovement);
            else
                waveSpeed = Mathf.SmoothStep(movSpeedWandering, 0, timerWave / (timeWaveMovement*2));



        }
        else
        {
            status = Statuses.GoingAway;
            targetDistance = distanceToPlayer;
            distanceToPlayer = Vector2.Distance(transform.position, playerParentTransform.position);
            timer = 0;
        }
    }

    private void Escape()
    {
        float distanceToAchieve = Mathf.SmoothStep(distanceToPlayer, targetDistance, timer / timeToArriveAndScape);
        Vector2 newPosition = GetPointDistanceFromObject(distanceToAchieve, (transform.position - playerParentTransform.position).normalized, playerParentTransform.position);
        transform.position = newPosition;
        if (distanceToAchieve == targetDistance)
        {
            Destroy(gameObject);
        }
    }

    Vector2 GetPointDistanceFromObject(float distanceFromSurface, Vector2 direction, Vector3 position)
    {
        Vector2 finalDirection = direction.normalized * distanceFromSurface;
        Vector2 targetPosition = position + Utils.Vec2To3(finalDirection);
        return targetPosition;
    }




}
