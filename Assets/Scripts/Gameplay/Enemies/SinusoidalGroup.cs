using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SinusoidalGroup : MonoBehaviour, ILevelable
{
    [SerializeField] GameObject ship;
    [SerializeField] int minNumberOfShips = 6;
    [SerializeField] int maxNumberOfShips = 10;
    [SerializeField] float timeBetweenShipGeneration = 0.2f;

    float verticalOffset = -10;
    int numberOfTotalShips;
    int numberOfGeneratedShips;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
		var playerParentTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
		transform.parent = playerParentTransform;

		numberOfTotalShips = Random.Range(minNumberOfShips, maxNumberOfShips + 1);
        numberOfGeneratedShips = 0;
        verticalOffset = Random.Range(verticalOffset,1);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenShipGeneration && numberOfGeneratedShips < numberOfTotalShips)
        {
            Vector3 eulerRotation = Camera.main.transform.rotation.eulerAngles;
            eulerRotation = new Vector3(eulerRotation.x, eulerRotation.y, eulerRotation.z + 90);

            GameObject gameObject = Instantiate(ship, transform.position, Quaternion.Euler(eulerRotation));
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

	void ILevelable.SetLevel(int level)
	{
		minNumberOfShips = level;
		maxNumberOfShips = level + 1;
	}
}
