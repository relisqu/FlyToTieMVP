using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Generation
{
    public class BackgroundDecoration : MonoBehaviour
    {
        [SerializeField] [Range(0, 1f)] private float RotationChance = 0.7f;
        [SerializeField] [Range(0, 1f)] private float EnableChance = 0.7f;

        [SerializeField] private BackgroundDecoration ChainedDecoration;
        private bool _enabledDecoration;
        public bool EnabledDecoration => _enabledDecoration;

        private void OnEnable()
        {
            _enabledDecoration = Random.value < EnableChance;
            if (ChainedDecoration != null && ChainedDecoration.EnabledDecoration)
            {
                _enabledDecoration = true;
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(_enabledDecoration);
                var isRotated = RotationChance >= EnableChance ? -1f : 1f;
                var localScale = transform.localScale;
                localScale = new Vector3(isRotated * localScale.x, localScale.y, localScale.z);
                transform.localScale = localScale;
            }
        }
    }
}