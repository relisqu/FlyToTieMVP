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


        private static List<Unit> _currentUnitList = new();

        public void GenerateUnitList()
        {
            _currentUnitList.Clear();

            Debug.Log("Starting adding unit to list");
            foreach (var unit in Units)
            {
                if (unit.FromLevel <= PlayerData.СurrentLevel)
                {
                    Debug.Log(unit.name);
                    _currentUnitList.Add(unit.Unit);
                }
            }
        }

        void Spawn()
        {
            var randomUnit = _currentUnitList[Random.Range(0, _currentUnitList.Count)];
            var obj = Instantiate(randomUnit.gameObject, Vector3.zero, Quaternion.identity, SpotPlace);
            obj.transform.localPosition = Vector3.zero;
        }

        private void Start()
        {
            PlayerData.ChangedLevel += GenerateUnitList;
            GenerateUnitList();
            Spawn();
        }

        private void OnDestroy()
        {
            PlayerData.ChangedLevel -= GenerateUnitList;
        }
    }
}