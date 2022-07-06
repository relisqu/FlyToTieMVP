using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Obstacle;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private float MinDistanceBetweenChunks;
        [SerializeField] private float MaxDistanceBetweenChunks;
        [SerializeField] private float LevelWidth;
        [SerializeField] private float Height;

        [SerializeField] private Tilemap GroundTilemap;
        [SerializeField] private ChunkGenerator ChunkGenerator;
        [SerializeField] private float ChanceToGenerateLaser;
        [SerializeField] private float MinimumLaserDistance;
        [SerializeField] private float MaximumLaserDistance;
        [SerializeField] private LaserEditor Laser;
        [SerializeField] private TileBase Tile;


        [SerializeField] private Transform GarbageTransform;
        private float previousWidth;

        [Button("Generate Islands")]
        public void GenerateIslands()
        {
            GroundTilemap.ClearAllTiles();
            places.Clear();
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

        public PlaceForObject GetSpace(Tilemap tilemap, Vector3Int position)
        {
            Vector3Int topTile = Vector3Int.zero;
            Vector3Int bottomTile = Vector3Int.zero;
            var newPosition = position;
            var maxNumber = 15;
            var number = 0;
            while (tilemap.GetTile(newPosition) == null && number < maxNumber)
            {
                number++;
                newPosition += Vector3Int.up;
                topTile = newPosition;
                //   tilemap.SetTile(newPosition, Tile);
            }

            if (number == maxNumber)
            {
                topTile = Vector3Int.zero;
            }

            newPosition = position;
            number = 0;
            while (tilemap.GetTile(newPosition) == null && number < maxNumber)
            {
                number++;
                newPosition -= Vector3Int.up;
                bottomTile = newPosition;
                //   tilemap.SetTile(newPosition, Tile);
            }


            if (number == maxNumber)
            {
                bottomTile = Vector3Int.zero;
            }

            if (bottomTile == Vector3Int.zero || topTile == Vector3Int.zero)
            {
                return null;
            }

            return new PlaceForObject(topTile, bottomTile);
        }


        public List<PlaceForObject> places = new List<PlaceForObject>();
        public float sizeChange;

        public void GenerateLasers()
        {
            var currentX = 0;
            var previousXValid = false;
            while (currentX < LevelWidth)
            {
                for (int i = 1; i < 10; i++)
                {
                    var position = new Vector3Int((int) currentX, (int) (Height / 2 - Height * (1 - 1f / i)), 10);
                    var place = GetSpace(GroundTilemap, position);
                    if (place != null && place.IsValid())
                    {
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

                        places.Add(place);
                        var topPoint = GroundTilemap.CellToWorld(place.TopPoint);
                        var BottomPoint = GroundTilemap.CellToWorld(place.BottomPoint);
                        var newLaser = Instantiate(Laser, (topPoint + BottomPoint )/ 2f + new Vector3(0.5f, 0.5f, 0f),
                            Quaternion.identity, GarbageTransform);
                        newLaser.ChangeRendererYSize(place.TopPoint.y - place.BottomPoint.y - sizeChange);
                        currentX += 5;
                        break;
                    }

                    previousXValid = false;
                }

                currentX++;

                /*

                var position = GroundTilemap.CellToWorld(new Vector3Int((int) currentX, (int) Height / 2, 10));
                var bottomHit = Physics2D.Raycast(position, Vector2.up, Height / 2);
                var topHit = Physics2D.Raycast(position, Vector2.down, Height / 2);
                Debug.DrawLine(bottomHit.point, topHit.point);
                if (bottomHit && topHit)
                {
                    print(bottomHit.transform.name + " " + topHit.transform.name);
                    print("Eba");
                    if (Random.value < ChanceToGenerateLaser)
                    {
                        Instantiate(Laser, Vector3.Lerp(topHit.point, bottomHit.point, 0.5f), quaternion.identity,
                            GarbageTransform);
                        Laser.ChangeRendererYSize(-topHit.point.y + bottomHit.point.y);
                        print(topHit.point + " " + bottomHit.point);
                        currentX += Random.Range(MinimumLaserDistance, MaximumLaserDistance);
                    }
                }

                currentX += 1f;
                */
            }
        }
    }

    public class PlaceForObject
    {
        public Vector3Int TopPoint;
        public Vector3Int BottomPoint;
        public bool isFilled;

        public PlaceForObject(Vector3Int cellBoundsMIN, Vector3Int cellBoundsMAX)
        {
            TopPoint = cellBoundsMIN;
            BottomPoint = cellBoundsMAX;
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