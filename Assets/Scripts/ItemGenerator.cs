using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
	[System.Serializable]
	public class GeneratedItem
	{
		public int chance;
		public GameObject item;
	}

	[SerializeField] GeneratedItem[] Items;
    [SerializeField] float timeToSpawnNewItem = 2;
	[SerializeField] int level = 1;
	float timer;
	int chanceSum;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
		chanceSum = Items.Sum(i => i.chance);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToSpawnNewItem)
        {
			// +1 because max in range is exclusive
			var val = Random.Range(1, chanceSum + 1);

			Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
			spawnPosition = spawnPosition + Camera.main.transform.right * 20 + Camera.main.transform.up * Random.Range(-10, 1);

			var newItem = GetObjectForVal(val);
			var levelable = newItem.GetComponent<ILevelable>();
			if (levelable != null) levelable.SetLevel(level);
			Instantiate(newItem, spawnPosition, Quaternion.identity);

            timer = 0;
        }

    }

	GameObject GetObjectForVal(int val)
	{
		int cummulative = 1;

		foreach(var i in Items)
		{
			if (val < cummulative + i.chance)
				return i.item;

			cummulative += i.chance;
		}

		return null;
	}
}
