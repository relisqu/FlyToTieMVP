using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerWallFollow : MonoBehaviour
    {
        [SerializeField] private PlayerMovement Player;


        private void Update()
        {
            var playerXPosition = Player.transform.position.x;
            transform.position = new Vector3(playerXPosition, transform.position.y, 0f);
        }
    }
}