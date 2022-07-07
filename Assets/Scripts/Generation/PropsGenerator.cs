using System.Collections.Generic;
using Player;
using Scripts.Obstacle;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class PropsGenerator : MonoBehaviour
    {
        [SerializeField] private LevelGenerator Level;
        [SerializeField] private List<Unit> Units;
        [SerializeField] private float ChanceToGenerateUnit;
        [SerializeField] private float MinimumDistanceBetweenUnits;
        [SerializeField] private Transform GarbageTransform;

        [SerializeField] private List<GameObject> BoxObjects;
        [SerializeField] private Vector3 DefaultBoxPosition;
        [SerializeField] private float MinimumDistanceBetweenBoxes;
        [SerializeField] private float MaximumDistanceBetweenBoxes;
        [SerializeField] private float ChanceToGenerateBoxes;
        [SerializeField] private int BoxesGenerateFromLevel;


        [SerializeField] private float ChanceToGenerateEnemies;
        [SerializeField] private Narval Enemy;
        [SerializeField] private float MinimumEnemyDistance;

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

                Instantiate(GetAppropriateUnit(), position, Quaternion.identity,
                    GarbageTransform);
                lastPosition = position;
                place.isFilled = true;
            }
        }

        public void GenerateEnemies()
        {
            var width = Level.GetWidth();
            var newPosition = DefaultBoxPosition;
            print("AA");
            while (newPosition.x < DefaultBoxPosition.x + width)
            {
                if (!Physics2D.OverlapCapsule(newPosition + Vector3.right * (0.5f * Enemy.GetTriggerDistance()),
                    new Vector2(0.4f, 1.2f) * Enemy.GetTriggerDistance(), CapsuleDirection2D.Horizontal, 0))
                {
                    if (Random.value > ChanceToGenerateEnemies)
                    {
                        continue;
                    }

                    Instantiate(Enemy, newPosition + Vector3.right * Enemy.GetTriggerDistance(), Quaternion.identity,
                        GarbageTransform);
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

                            var randomBox = BoxObjects[Random.Range(0, BoxObjects.Count)];
                            Instantiate(randomBox, newPosition, quaternion.identity, GarbageTransform);
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
            var unit = Units[Random.Range(0, Units.Count)];
            while (unit.SpawnsFromLevel > level)
            {
                unit = Units[Random.Range(0, Units.Count)];
            }

            return unit;
        }
    }
}