using System;
using System.Collections;
using Player;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class СoinPerEnemyText : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text CoinText;

        [SerializeField] private int CoinPerUnitAmount;
        [SerializeField] private EndCutscene EndCutscene;


        public IEnumerator GenerateText()
        {
            CoinText.SetText("");
            var money = CoinPerUnitAmount;
            var fullMoney = money;
            Unit unit = StarterUnit.Instance;
            CoinText.text += "+" + money + " <sprite=2> \n\n";
            unit = unit.GetBelowUnit();
            while (unit!=null)
            {
                yield return new WaitForSeconds(0.5f);
                money += CoinPerUnitAmount;
                fullMoney += money;
                CoinText.text += "+" + money + " <sprite=2> \n\n";
                unit = unit.GetBelowUnit();
            }

            yield return new WaitForSeconds(1f);
            PlayerData.SaveMoney(PlayerData.MoneyCount + fullMoney);
            AudioManager.instance.Play("coin");
            EndCutscene.DestroyMoneyText();
        }

        private void OnEnable()
        {
            StartCoroutine(GenerateText());
        }
    }
}