using System;
using Player;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class CurrentLevelText : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text LevelText;

        void UpdateText()
        {
            LevelText.SetText(PlayerData.СurrentLevel.ToString());
        }

        private void OnEnable()
        {
            PlayerData.ChangedLevel += UpdateText;
        }

        private void OnDisable()
        {
            PlayerData.ChangedLevel -= UpdateText;
        }
    }
}