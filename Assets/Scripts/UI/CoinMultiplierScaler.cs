using System;
using System.Globalization;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class CoinMultiplierScaler : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text CoinText;
        private float scale = 1f;

        public static CoinMultiplierScaler Instance;

        private void Start()
        {
            CoinText.SetText("");
            PlayerMovement.OnBigJump += UpdateText;
            PlayerMovement.OnSmallJump += UpdateText;
        }

        private void Awake()
        {
            
            Instance = this;
        }

        public bool IsEnabled;

        public void Enable()
        {
            IsEnabled = true;
            CoinText.SetText("x1<sprite=2>");
            UpdateText();
        }

        private void OnDestroy()
        {
            PlayerMovement.OnBigJump -= UpdateText;
            PlayerMovement.OnSmallJump -= UpdateText;
        }

        public void UpdateText()
        {
            if (IsEnabled)
            {
                if (scale % 1 == 0)
                {
                    CoinText.SetText($"x{Mathf.RoundToInt(scale)}<sprite=2>");
                }

                else
                {
                    CoinText.SetText($"x{scale.ToString("F1", CultureInfo.InvariantCulture)}<sprite=2>");
                }
            }
            else
            {
                CoinText.SetText("");
            }
        }


        public float GetScale()
        {
            return scale;
        }

        public void SetScale(float scale)
        {
            this.scale = scale;
            UpdateText();
        }
    }
}