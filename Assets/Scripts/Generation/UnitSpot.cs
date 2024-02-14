using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class UnitSpot : MonoBehaviour
    {
        [SerializeField] private List<UnitData> Units = new();
        [SerializeField] private Transform SpotPlace;


        private List<Unit> _currentUnitList;

        void GenerateUnitList()
        {
            _currentUnitList.Clear();

            foreach (var unit in Units)
            {
                if (unit.FromLevel <= PlayerData.СurrentLevel)
                {
                    _currentUnitList.Add(unit.Unit);
                }
            }
        }

        void Spawn()
        {
            var randomUnit = _currentUnitList[Random.Range(0, _currentUnitList.Count)];
            Instantiate(randomUnit, SpotPlace.position, Quaternion.identity, null);
        }

        private void Start()
        {
            PlayerData.ChangedLevel += GenerateUnitList;
            Spawn();
        }

        private void OnDestroy()
        {
            PlayerData.ChangedLevel -= GenerateUnitList;
        }
    }
}