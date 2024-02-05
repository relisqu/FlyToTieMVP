using System;
using DefaultNamespace.Generation;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class GameStart : MonoBehaviour
    {
        private void Start()
        {
            LevelGenerator.Instance.SpawnLevel();
            AudioManager.instance.Play("music");
        }
    }
}