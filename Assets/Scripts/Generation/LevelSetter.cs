using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.Generation
{
    public class LevelSetter : MonoBehaviour
    {
        public int Neededlevel;

        [Button]
        public void SetLevel()
        {
            PlayerData.SaveLevel(Neededlevel);
        }
    }
}