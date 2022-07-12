using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class GameStart : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.instance.Play("music");
        }
    }
}