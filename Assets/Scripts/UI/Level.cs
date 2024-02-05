using System;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private int Offset;

        [BoxGroup("Text")] [SerializeField] private TMPro.TMP_Text Text;
        [BoxGroup("Text")] [SerializeField] private Color CurrentLevelTextColor;
        [BoxGroup("Text")] [SerializeField] private Color DefaultLevelTextColor;
        [BoxGroup("Icon")] [SerializeField] private Image LevelSpriteRenderer;
        [BoxGroup("Bridge")] [SerializeField] private Image BridgeSpriteRenderer;
        [BoxGroup("Icon")] [SerializeField] private Sprite CurrentLevelSprite;
        [BoxGroup("Icon")] [SerializeField] private Sprite CompletedLevelSprite;
        [BoxGroup("Icon")] [SerializeField] private Sprite NotCompletedLevelSprite;
        [BoxGroup("Bridge")] [SerializeField] private Sprite CompletedBridgeSprite;
        [BoxGroup("Bridge")] [SerializeField] private Sprite NotCompletedBridgeSprite;

        int GetNumber()
        {
            var number = (PlayerData.СurrentLevel - 1) / 4 * 4 + Offset;

            return Mathf.Clamp(number, 0, number);
        }

        private void OnEnable()
        {
            UpdateSprite();
        }

        public void Start()
        {
            UpdateSprite();
        }

        public void UpdateSprite()
        {
            var level = GetNumber();
            transform.localScale = Vector3.one;

            var playerLevel = PlayerData.СurrentLevel;
            Text.SetText((level).ToString());
            if (level < playerLevel)
            {
                Text.color = DefaultLevelTextColor;
                LevelSpriteRenderer.sprite = CompletedLevelSprite;
                BridgeSpriteRenderer.sprite = CompletedBridgeSprite;
                return;
            }

            if (level == playerLevel)
            {
                Text.color = CurrentLevelTextColor;
                LevelSpriteRenderer.sprite = CurrentLevelSprite;
                BridgeSpriteRenderer.sprite = NotCompletedBridgeSprite;
                transform.localScale = Vector3.one * 1.23f;
                return;
            }

            Text.color = CurrentLevelTextColor;
            LevelSpriteRenderer.sprite = NotCompletedLevelSprite;
            BridgeSpriteRenderer.sprite = NotCompletedBridgeSprite;
        }
    }
}