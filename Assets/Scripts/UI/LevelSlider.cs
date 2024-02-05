using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class LevelSlider : MonoBehaviour
    {
        [SerializeField] private Slider Slider;
        [SerializeField] private Transform EndLevelTransform;
        [SerializeField] private Transform StartLevelTransform;
        private float _width;

        private void Start()
        {
            _width = EndLevelTransform.position.x - StartLevelTransform.position.x;
        }

        private void Update()
        {
            Slider.value = Mathf.Clamp01((StarterUnit.Instance.transform.position.x - StartLevelTransform.position.x) /
                                         _width);
        }
    }
}