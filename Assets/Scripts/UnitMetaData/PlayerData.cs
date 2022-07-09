using System;
using DefaultNamespace.UI;
using UnityEngine;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        public static int MoneyCount = 0;
        public static int СurrentLevel = 3;
        public static float СurrentBulletProjectileSpeed = 30;
        public static int СurrentBulletProjectileAmount = 2;
        
        public static Action ChangedMoneyCount;
        public static Action ChangedLevel;

        public static void SaveMoney(int newMoneyCount)
        {
            MoneyCount = newMoneyCount;
            PlayerPrefs.SetInt("Money", MoneyCount);
            ChangedMoneyCount?.Invoke();
        }

        public static void SaveLevel(int newLevel)
        {
            СurrentLevel = newLevel;
            PlayerPrefs.SetInt("Level", СurrentLevel);
            ChangedLevel?.Invoke();
        }

        public static void LoadVariables()
        {
            MoneyCount = PlayerPrefs.GetInt("Money", 0);
            СurrentLevel = PlayerPrefs.GetInt("Level", 1);
            СurrentLevel++;
            SaveLevel(СurrentLevel);
        }

        private void Awake()
        {
            LoadVariables();
        }
    }
}