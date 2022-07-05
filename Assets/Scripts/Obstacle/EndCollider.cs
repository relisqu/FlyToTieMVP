using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Obstacle
{
    public class EndCollider : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Unit unit))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}

