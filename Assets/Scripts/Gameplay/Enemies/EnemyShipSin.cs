using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSin : EnemyDamageable
{

    [SerializeField] float frequency = 3.0f;
    [SerializeField] float magnitudeWaveMovement = 1f;
    private Vector3 waveDirection = Vector3.up;
    private Vector3 velocity = Vector3.zero;
    private float sinFactor;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
		base.Start();
    }

    private void Update()
    {
        waveDirection = Camera.main.transform.up;
        base.Update();
        pos = transform.position;
        sinFactor = Mathf.Sin((Time.timeSinceLevelLoad - initializationTime) * frequency);
        transform.position = pos + waveDirection * sinFactor * magnitudeWaveMovement * Time.deltaTime;
    }


}
