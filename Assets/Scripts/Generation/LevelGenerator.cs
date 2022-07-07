using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Obstacle;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class LevelGenerator : MonoBehaviour
    {
        [BoxGroup("Islands")] [SerializeField] private float MinDistanceBetweenChunks;
        [BoxGroup("Islands")] [SerializeField] private float MaxDistanceBetweenChunks;

        [BoxGroup("Level constrains")] [SerializeField]
        private float LevelWidth;

        [BoxGroup("Level constrains")] [SerializeField]
        private float Height;

        [BoxGroup("Level constrains")] [SerializeField]
        private Tilemap GroundTilemap;

        [BoxGroup("Islands")] [SerializeField] private ChunkGenerator ChunkGenerator;
        [BoxGroup("Lasers")] [SerializeField] private float ChanceToGenerateLaser;
        [BoxGroup("Lasers")] [SerializeField] private float MinimumLaserDistance;
        [BoxGroup("Lasers")] [SerializeField] private float MaximumLaserDistance;
        [BoxGroup("Lasers")] [SerializeField] private LaserEditor Laser;

        [BoxGroup("Level constrains")] [SerializeField]
        private Transform GarbageTransform;

        [BoxGroup("Level constrains")] [SerializeField]
        private CompositeCollider2D Collider;

        [BoxGroup("Lasers")] [FormerlySerializedAs("sizeChange")] [SerializeField]
        private float LasersSizeOffset;

        public List<PlaceForObject> Places = new();
        public List<PlaceForObject> OpenPlaces = new();


        [Button("Generate Islands")]
        public void GenerateIslands()
        {
            GroundTilemap.ClearAllTiles();
            Places.Clear();
            OpenPlaces.Clear();
            ChunkGenerator.InitiateChunks();

            var currentX = 0f;
            while (currentX < LevelWidth)
            {
                var tilemapChunks = ChunkGenerator.GenerateOneCombination().OrderBy(x => Guid.NewGuid()).ToList();
                var shift = Random.Range(MinDistanceBetweenChunks, MaxDistanceBetweenChunks);
                if (tilemapChunks.Count == 1)
                {
                    currentX += tilemapChunks[0].Width / 2f + shift;
                    var randomY = Height / 2 + Random.Range(-Height / 4, Height / 4);
                    var positionShift = new Vector3Int((int) currentX, (int) randomY, 10);
                    AddTilesToTilemapChunks(tilemapChunks[0], positionShift);
                    currentX += tilemapChunks[0].Width / 2f;
                }
                else if (tilemapChunks.Count == 2)
                {
                    var biggestWidth = Mathf.Max(tilemapChunks[0].Width, tilemapChunks[1].Width);
                    currentX += biggestWidth / 2f + shift;
                    var topY = Height / 2 + tilemapChunks[0].Height / 2f + Random.Range(Height / 6, Height / 5);
                    var bottomY = Height / 2 - tilemapChunks[1].Height / 2f - Random.Range(Height / 6, Height / 5);
                    AddTilesToTilemapChunks(tilemapChunks[0],
                        new Vector3Int((int) currentX, (int) topY, 10) + GenerateOffset(5, 2));
                    AddTilesToTilemapChunks(tilemapChunks[1], new Vector3Int((int) currentX, (int) bottomY, 10));
                    currentX += biggestWidth / 2f;
                }
                else if (tilemapChunks.Count == 3)
                {
                    var biggestWidth = Mathf.Max(tilemapChunks[0].Width, tilemapChunks[1].Width,
                        tilemapChunks[2].Width);
                    currentX += biggestWidth / 2f + shift;
                    var topY = Height / 2 + tilemapChunks[0].Height / 2f + Random.Range(Height / 5, Height / 4) +
                               tilemapChunks[1].Height / 2f;
                    var middleY = Height / 2 + Random.Range(-Height / 7, Height / 7);
                    var bottomY = Height / 2 - tilemapChunks[2].Height / 2f - Random.Range(Height / 5, Height / 4) -
                                  tilemapChunks[1].Height / 2f;
                    AddTilesToTilemapChunks(tilemapChunks[0],
                        new Vector3Int((int) currentX, (int) topY, 10) + GenerateOffset(3, 0));
                    AddTilesToTilemapChunks(tilemapChunks[1],
                        new Vector3Int((int) currentX, (int) middleY, 10) + GenerateOffset(5, 2));
                    AddTilesToTilemapChunks(tilemapChunks[2],
                        new Vector3Int((int) currentX, (int) bottomY, 10) + GenerateOffset(3, 0));
                    currentX += biggestWidth / 2f;
                }
            }

            Collider.GenerateGeometry();
        }

        private Vector3Int GenerateOffset(int xBounds, int yBounds)
        {
            return new Vector3Int(Random.Range(-xBounds, xBounds), Random.Range(-yBounds, yBounds), 0);
        }

        void AddTilesToTilemapChunks(TilemapChunk chunk, Vector3Int position)
        {
            chunk.GenerateTiles();
            foreach (var tile in chunk.tileWorldLocations)
            {
                //  print(tile + position);
                GroundTilemap.SetTile(tile + position, chunk.Tilemap.GetTile(tile));
            }
        }

        private float _minSize = 5;

        public PlaceForObject GetSpace(Tilemap tilemap, Vector3Int position, int minTopNumber, int maxTopNumber,
            int minBottomNumber, int maxBottomNumber)
        {
            Vector3Int topTile = Vector3Int.zero;
            Vector3Int bottomTile = Vector3Int.zero;
            var newPosition = position;
            var number = 0;
            while (tilemap.GetTile(newPosition) == null && number < maxTopNumber)
            {
                number++;
                newPosition += Vector3Int.up;
                topTile = newPosition;
            }

            if (number == maxTopNumber || number < minTopNumber)
            {
                topTile = Vector3Int.zero;
            }

            newPosition = position;
            number = 0;
            while (tilemap.GetTile(newPosition) == null && number < maxBottomNumber)
            {
                number++;
                newPosition -= Vector3Int.up;
                bottomTile = newPosition;
            }


            if (number == maxBottomNumber || number < minBottomNumber)
            {
                bottomTile = Vector3Int.zero;
            }

            if (bottomTile == Vector3Int.zero || topTile == Vector3Int.zero)
            {
                return null;
            }

            return new PlaceForObject(topTile, bottomTile, GroundTilemap);
        }


        public void GenerateLasers()
        {
            var currentX = 0;
            var previousXValid = false;
            while (currentX < LevelWidth)
            {
                for (int i = 1; i < 10; i++)
                {
                    var position = new Vector3Int((int) currentX, (int) (Height / 2 - Height * (1 - 1f / i)), 10);
                    var place = GetSpace(GroundTilemap, position, 0, 15, 0, 15);
                    if (place != null && place.IsValid())
                    {
                        Places.Add(place);
                        if (Random.value > ChanceToGenerateLaser)
                        {
                            currentX += 5;
                            previousXValid = false;
                            break;
                        }

                        if (!previousXValid)
                        {
                            previousXValid = true;
                            currentX += Random.Range(0, 2);
                            break;
                        }

                        var newLaser = Instantiate(Laser,
                            (place.WorldTopPoint + place.WorldBottomPoint) / 2f + new Vector3(0.5f, 0.5f, 0f),
                            Quaternion.identity, GarbageTransform);
                        place.isFilled = true;
                        newLaser.ChangeRendererYSize(place.TopPoint.y - place.BottomPoint.y - LasersSizeOffset);
                        currentX += (int) Random.Range(MinimumLaserDistance, MaximumLaserDistance);
                        break;
                    }

                    previousXValid = false;
                }

                currentX++;
            }
        }

        private float _previousWidth;

        public float GetWidth()
        {
            return LevelWidth;
        }
    }

    public class PlaceForObject
    {
        public Vector3Int TopPoint;
        public Vector3Int BottomPoint;
        public Vector3 WorldTopPoint;
        public Vector3 WorldBottomPoint;
        public bool isFilled;

        public PlaceForObject(Vector3Int cellBoundsMIN, Vector3Int cellBoundsMAX, Tilemap map)
        {
            TopPoint = cellBoundsMIN;
            BottomPoint = cellBoundsMAX;
            WorldTopPoint = map.CellToWorld(TopPoint);
            WorldBottomPoint = map.CellToWorld(BottomPoint);
        }

        public float GetMagnitude()
        {
            return Vector3.Distance(TopPoint, BottomPoint);
        }

        public bool IsValid()
        {
            return Vector3.Distance(TopPoint, BottomPoint) >= 5f;
        }
    }
}