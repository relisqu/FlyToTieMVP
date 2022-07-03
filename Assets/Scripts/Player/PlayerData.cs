using UnityEngine;

namespace Player
{
    public class PlayerData : MonoBehaviour
    {
        public static int MoneyCount=0;
        public static int СurrentLevel=3;

        void SaveData()
        {
            PlayerPrefs.SetInt("Money",MoneyCount);
        }

    }
}