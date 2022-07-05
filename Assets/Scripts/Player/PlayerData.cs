using System;
using UnityEngine;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        public static int MoneyCount = 0;
        public static int СurrentLevel = 3;
        public static Action ChangedMoneyCount;
        public static void SaveMoney(int newMoneyCount)
        {
            MoneyCount = newMoneyCount;
            PlayerPrefs.SetInt("Money", MoneyCount);
            ChangedMoneyCount?.Invoke();
        }

        public static void LoadMoney()
        {
            MoneyCount = PlayerPrefs.GetInt("Money", 0);
        }

        private void Awake()
        {
            LoadMoney();
        }
    }
}