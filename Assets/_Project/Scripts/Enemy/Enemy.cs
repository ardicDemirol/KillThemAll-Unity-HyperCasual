using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;

        [SerializeField] private PlayerCrowd playerCrowd;

        private int _enemyCount;

        private void Awake()
        {
            animators = GetComponentsInChildren<Animator>();
            _enemyCount = animators.Length;
        }

        //---------------------------------------------------------------------------
        public void TakeDamage(int playerCount)
        {
            int leftPlayerCount = playerCount - _enemyCount;
            //leftEnemyCount = (int)Mathf.Lerp(0, _enemyCount, leftEnemyCount);

            if (leftPlayerCount > 0)
            {
                for (int i = 0; i < _enemyCount; i++)
                {
                    animators[i].SetBool("isDead", true);
                }
                //Debug.Log("Player Win");
            }
            else
            {
                for (int i = 0; i < playerCount; i++)
                {
                    animators[i].SetBool("isDead", true);
                }
                //Debug.Log("Player Lose");
            }

            //_enemyCount -= playerCount;
            //GetComponent<Collider>().enabled = false;
            //GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.gray);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                int firstEnemyCount = _enemyCount;
                TakeDamage(playerCrowd.Shooters.Count);

                var damageable = other.gameObject.GetComponent<IRemoveShooter>();
                damageable.RemoveShooterBossFight(firstEnemyCount);
            }
        }
    }
}