using System;
using System.Collections;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace DefaultNamespace.UI
{
    public class Gift : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private int MinCoinCount;
        [SerializeField] private int MaxCoinCount;
        [SerializeField] private int Step;
        [SerializeField] private TMPro.TMP_Text RewardText;
        [SerializeField] private Transform GiftObject;

        public int GenerateReward()
        {
            var steps = (MaxCoinCount - MinCoinCount) / Step;
            var reward = Random.Range(0, steps);
            return MinCoinCount + reward * Step;
        }

        private void OnEnable()
        {
            WasGiftOpened = false;
            GiftObject.localScale = Vector3.one;
            RewardText.SetText("");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!WasGiftOpened) StartCoroutine(OpenGift());
        }

        public IEnumerator OpenGift()
        {
            WasGiftOpened = true;
            var reward = GenerateReward();
            RewardText.transform.localScale=Vector3.zero;
            RewardText.transform.DOScale(Vector3.one,0.8f);
            RewardText.SetText("+" + reward + " <sprite=2>");
            var mySequence = DOTween.Sequence();
            mySequence.Append(
                    GiftObject.DOShakePosition(0.5f, 15f))
                .Append(GiftObject.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutBounce));

            yield return mySequence.Play();
            PlayerData.SaveMoney(PlayerData.MoneyCount + reward);
            yield return new WaitForSeconds(2f);
            OpenedGift?.Invoke();
        }

        private void OnDisable()
        {
            WasGiftOpened = false;
            GiftObject.localScale = Vector3.one;
        }

        public static bool WasGiftOpened;

        public static Action OpenedGift;
    }
}