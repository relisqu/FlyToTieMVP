using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.Meta
{
    public class MetaUpgradeButton : MonoBehaviour
    {
        [SerializeField] private UnitData Data;

        [Header("Transforms and buttons")] [SerializeField]
        private Transform ClosedMenu;

        [SerializeField] private TMPro.TMP_Text ClosedMenuLevelStart;

        [SerializeField] private Transform UpgradeMenu;
        [SerializeField] private TMPro.TMP_Text DescriptionText;
        [SerializeField] private TMPro.TMP_Text NameText;
        [SerializeField] private TMPro.TMP_Text CoinCountText;
        [SerializeField] private Button CoinCountButton;


        private int _curUpgradeLevel;

        private void Start()
        {
            SetTextContext();
        }

        public void SetTextContext()
        {
            _curUpgradeLevel = Data.LoadLevel() < Data.LevelsUpgrade.Count - 1
                ? Data.LoadLevel()
                : Data.LevelsUpgrade.Count - 1;
            var areUpgradesAvailable = PlayerData.СurrentLevel >= Data.FromLevel;
            ClosedMenuLevelStart.SetText($"Reach level {Data.FromLevel} to unlock");

            ClosedMenu.gameObject.SetActive(!areUpgradesAvailable);
            UpgradeMenu.gameObject.SetActive(areUpgradesAvailable);

            if (_curUpgradeLevel >= Data.LevelsUpgrade.Count - 1)
            {
                NameText.SetText($"{Data.Name}VMax");
            }
            else
            {
                NameText.SetText($"{Data.Name}V{_curUpgradeLevel + 1}");
            }

            DescriptionText.SetText(Data.LevelsUpgrade[_curUpgradeLevel].Description);

            var coinUpgradeCount = Data.LevelsUpgrade[_curUpgradeLevel].UpgradeMoneyCount;
            CoinCountText.SetText($"{coinUpgradeCount} <sprite=2 tint=1>");

            if (PlayerData.MoneyCount < coinUpgradeCount)
            {
                CoinCountButton.interactable = false;
                CoinCountText.color = Color.white * 0.525f;
            }
            else
            {
                CoinCountButton.interactable = true;
                CoinCountText.color = Color.white;
            }

            if (Data.LoadLevel() >= Data.LevelsUpgrade.Count - 1)
                CoinCountButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            SetTextContext();
        }

        public void UpdateButton()
        {
            if (PlayerData.MoneyCount < Data.LevelsUpgrade[_curUpgradeLevel].UpgradeMoneyCount) return;

            PlayerData.SaveMoney(PlayerData.MoneyCount - Data.LevelsUpgrade[_curUpgradeLevel].UpgradeMoneyCount);
            Data.SaveLevel(Data.LoadLevel() + 1);
            SetTextContext();
        }
    }
}