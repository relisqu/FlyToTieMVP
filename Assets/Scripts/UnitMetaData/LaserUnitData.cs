using System;
using UnityEngine;

namespace UnitMetaData
{
    public class LaserUnitData : MonoBehaviour
    {
        public static LaserUnitData Instance;

        
        private void Start()
        {
            Instance = this;
        }
    }
}