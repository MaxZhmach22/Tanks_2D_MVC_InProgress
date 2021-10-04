using UnityEngine;

namespace Tanks
{
    internal class Projectile : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.isStatic)
            {
                Debug.Log(collision.gameObject.name);
                Destroy(gameObject);
            }

        }

    }
}
