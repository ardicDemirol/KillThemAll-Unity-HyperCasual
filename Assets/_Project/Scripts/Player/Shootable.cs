using UnityEngine;

namespace RakibJahan
{
    public class Shootable : MonoBehaviour
    {
        [SerializeField] private float shootableSurviveTime = 3f;
        private int _damagePerHit = 10;

        public void Init(int damagePerHit)
        {
            _damagePerHit = damagePerHit;
            Invoke(nameof(DestroyShootable), shootableSurviveTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Damageable"))
            {
                var damageable = collision.transform.GetComponentInChildren<IDamageable>();
                damageable.Damage(_damagePerHit);
                DestroyShootable();
            }
        }

        private void DestroyShootable()
        {
            Destroy(gameObject);
        }
    }
}