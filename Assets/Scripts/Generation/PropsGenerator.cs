using System;
using System.Collections.Generic;
using DefaultNamespace.Props;
using Player;
using Scripts.Obstacle;
using Units;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class PropsGenerator : MonoBehaviour
    {
        [SerializeField] private LevelGenerator Level;
        [SerializeField] private List<Unit> Units;
        [SerializeField] private List<UnitSpawn> UnitSpawn;
        [SerializeField] private float ChanceToGenerateUnit;
        [SerializeField] private float MinimumDistanceBetweenUnits;
        [SerializeField] private Transform GarbageTransform;

        [SerializeField] private List<Box> BoxObjects;
        [SerializeField] private Vector3 DefaultBoxPosition;
        [SerializeField] private float MinimumDistanceBetweenBoxes;
        [SerializeField] private float MaximumDistanceBetweenBoxes;
        [SerializeField] private float ChanceToGenerateBoxes;
        [SerializeField] private int BoxesGenerateFromLevel;


        [SerializeField] private float ChanceToGenerateEnemies;
        [SerializeField] private List<Narval> Enemy;
        [SerializeField] private float MinimumEnemyDistance;

        private void Start()
        {
            _unitPool = InstantiatePool<Unit>(Units, 30);
            _unitSpawnPointPool = InstantiatePool<UnitSpawn>(UnitSpawn, 30);
            _narvalPool = InstantiatePool<Narval>(Enemy, 30);
            _boxPool = InstantiatePool<Box>(BoxObjects, 40);
        }

        public void GenerateUnits()
        {
            Vector3 lastPosition = Vector3.negativeInfinity;
            foreach (var place in Level.Places)
            {
                if (place.isFilled) continue;
                if (Random.value > ChanceToGenerateUnit) continue;

                var position = (place.WorldTopPoint + place.WorldBottomPoint) / 2f;
                if (Vector3.Distance(position, lastPosition) < MinimumDistanceBetweenUnits)
                {
                    continue;
                }

                var unit = GetAppropriateUnit();
                unit.transform.position = position;
                unit.transform.parent = GarbageTransform;
                unit.gameObject.SetActive(true);


                var spawnObject = GetObjectFromPool(_unitSpawnPointPool);
                spawnObject.transform.position = position - Vector3.up * 0.2f;
                spawnObject.transform.parent = GarbageTransform;
                spawnObject.gameObject.SetActive(true);
                lastPosition = position;
                place.isFilled = true;
            }
        }

        private Unit[] _unitPool;
        private UnitSpawn[] _unitSpawnPointPool;
        private Narval[] _narvalPool;
        private Box[] _boxPool;

        public static T GetObjectFromPool<T>(T[] poolList) where T : MonoBehaviour
        {
            var obj = poolList[Random.Range(0, poolList.Length)];
            int number = 0;
            while (obj.gameObject.activeInHierarchy && number<100)
            {
                number++;
                obj = poolList[Random.Range(0, poolList.Length)];
            }

            return obj;
        }

        public static T[] InstantiatePool<T>(List<T> poolObject, int size) where T : MonoBehaviour
        {
            var poolList = new T[30];
            for (int i = 0; i < poolList.Length; i++)
            {
                poolList[i] = Instantiate(poolObject[Random.Range(0, poolObject.Count)]);
                poolList[i].gameObject.SetActive(false);
            }

            return poolList;
        }


        public void GenerateEnemies()
        {
            var width = Level.GetWidth();
            var newPosition = DefaultBoxPosition;
            while (newPosition.x < DefaultBoxPosition.x + width)
            {
                if (!Physics2D.OverlapCapsule(newPosition + Vector3.right * (0.5f * Enemy[0].GetTriggerDistance()),
                    new Vector2(0.4f, 1.2f) * Enemy[0].GetTriggerDistance(), CapsuleDirection2D.Horizontal, 0))
                {
                    if (Random.value > ChanceToGenerateEnemies)
                    {
                        continue;
                    }

                    var spawnObject = GetObjectFromPool(_narvalPool);
                    spawnObject.transform.position = newPosition + Vector3.right * spawnObject.GetTriggerDistance();
                    spawnObject.transform.parent = GarbageTransform;
                    spawnObject.gameObject.SetActive(true);

                    newPosition.y = DefaultBoxPosition.y + Random.Range(-4f, 2f);
                    newPosition += Vector3.right * Random.Range(MinimumEnemyDistance, MinimumEnemyDistance * 1.3f);
                }

                newPosition += Vector3.right;
            }
        }

        public void GenerateBoxes()
        {
            if (PlayerData.СurrentLevel < BoxesGenerateFromLevel) return;
            var width = Level.GetWidth();
            var newPosition = DefaultBoxPosition;
            while (newPosition.x < DefaultBoxPosition.x + width)
            {
                var hit = Physics2D.Raycast(newPosition, Vector2.down);
                if (hit.collider != null)
                {
                    float distance = Mathf.Abs(hit.point.y - newPosition.y);
                    if (distance > 2f && distance < 20f)
                    {
                        if (hit.collider.name == "Ground")
                        {
                            if (Random.value > ChanceToGenerateBoxes)
                            {
                                newPosition += Vector3.right;
                                continue;
                            }

                            var boxAmount = Random.Range(1, 3);
                            for (int i=0; i<boxAmount;i++) {
                                var spawnObject = GetObjectFromPool(_boxPool);
                                spawnObject.transform.position = newPosition+ Vector3.up * (0.5f * i);
                                spawnObject.transform.parent = GarbageTransform;
                                spawnObject.gameObject.SetActive(true);
                            }

                            newPosition += Vector3.right *
                                           Random.Range(MinimumDistanceBetweenBoxes, MaximumDistanceBetweenBoxes);
                            continue;
                        }
                    }
                }

                newPosition += Vector3.right;
            }
        }

        private Unit GetAppropriateUnit()
        {
            var level = PlayerData.СurrentLevel;
            var unit = _unitPool[Random.Range(0, _unitPool.Length)];
            while (unit.SpawnsFromLevel > level || unit.gameObject.activeInHierarchy
            )
            {
                unit = _unitPool[Random.Range(0, _unitPool.Length)];
            }

            return unit;
        }
    }
}