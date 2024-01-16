using UnityEngine;

namespace DefaultNamespace
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private string SoundName;

        public void PlaySound()
        {
            AudioManager.instance.Play(SoundName);
        }
    }
}