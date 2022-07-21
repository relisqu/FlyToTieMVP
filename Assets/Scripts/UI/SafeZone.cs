using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class SafeZone : MonoBehaviour
    {
        public Rect safeZone;

        public static SafeZone Instance;
        private void Awake()
        {
            Instance = this;
            safeZone = Screen.safeArea;
        }
        
        
    }
}