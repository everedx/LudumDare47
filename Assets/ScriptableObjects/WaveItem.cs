using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;


namespace ScriptableObjectLudum
{
    [CreateAssetMenu(fileName = "Wave Item", menuName = "Ludum/Wave Item", order = 1)]
    public class WaveItem : ScriptableObject
    {
        [SerializeField] public GameObject prefabToSpawn;
        [SerializeField] public int minQuantity;
        [SerializeField] public int maxQuantity;


    }


}

