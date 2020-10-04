using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstGroup : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] int minNumberOfShips = 1;
    [SerializeField] int maxNumberOfShips = 3;
    [SerializeField] float timeBetweenShipGeneration = 0.2f;

    private Transform parentTransform;
    int numberOfTotalShips;
    int numberOfGeneratedShips;
    float timer;
    List<float> offsets;
    // Start is called before the first frame update
    void Start()
    {
        numberOfTotalShips = Random.Range(minNumberOfShips, maxNumberOfShips + 1);
        numberOfGeneratedShips = 0;
        parentTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        offsets = GetPositionsOffsetsToSpawnShips(numberOfTotalShips);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenShipGeneration && numberOfGeneratedShips < numberOfTotalShips)
        {
            Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
            Vector3 eulerRotation = Camera.main.transform.rotation.eulerAngles;
            eulerRotation = new Vector3(eulerRotation.x, eulerRotation.y, eulerRotation.z + 90);

            GameObject gameObject = Instantiate(ship, spawnPosition + Camera.main.transform.right * 20 + Camera.main.transform.up * offsets[numberOfGeneratedShips], Quaternion.Euler(eulerRotation),parentTransform);
            ParticleSystem ps = gameObject.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                main.simulationSpace = ParticleSystemSimulationSpace.Custom;
                main.customSimulationSpace = Camera.main.transform;
            }


            numberOfGeneratedShips++;
            timer = 0;
        }
        else if (numberOfGeneratedShips >= numberOfTotalShips)
            Destroy(gameObject);
    }

    private List<float> GetPositionsOffsetsToSpawnShips(int numberOfShips)
    {
        List<float> listOfOffsets = new List<float>();
        float verticalOffset = 25;
        float distanceBetweenShips = (verticalOffset*2) / (numberOfShips + 1);
        for (float i = -verticalOffset+distanceBetweenShips; i < verticalOffset; i += distanceBetweenShips)
        {
            listOfOffsets.Add(i);
        }
        return listOfOffsets;
    }
}
