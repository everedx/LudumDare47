using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{

    [SerializeField] GameObject EnemyObject;
    [SerializeField] float timeToSpawnNewWave = 3;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToSpawnNewWave)
        {
            Instantiate(EnemyObject,Vector3.zero, Quaternion.identity);
               
                
            
            timer = 0;
        }

    }
}
