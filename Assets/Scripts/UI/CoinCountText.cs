using System;
using DG.Tweening;
using Player;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class CoinCountText : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text Text;
        [SerializeField] private Transform MoneyCanvas;

        private void OnEnable()
        {
            PlayerData.ChangedMoneyCount += UpdateMoneyCount;
            scale = MoneyCanvas.transform.localScale.x;
            UpdateMoneyCount();
        }

        private float scale;
        private void UpdateMoneyCount()
        {
            Text.SetText(PlayerData.MoneyCount.ToString());
            MoneyCanvas.DOPunchScale(Vector3.one * 0.3f, 0.2f).OnComplete(() =>
            {
                MoneyCanvas.transform.localScale = Vector3.one*scale;
            });
        }

        private void OnDisable()
        {
            PlayerData.ChangedMoneyCount += UpdateMoneyCount;
        }
    }
}