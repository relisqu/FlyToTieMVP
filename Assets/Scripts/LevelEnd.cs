using System;
using DefaultNamespace.UI;
using Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelEnd : MonoBehaviour
    {
        [SerializeField]private Cutscene Scene;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out StarterUnit unit))
            {
                Scene.PlayCutscene();
                PlayerData.SaveLevel(PlayerData.СurrentLevel+1);
            }
        }
    }
}