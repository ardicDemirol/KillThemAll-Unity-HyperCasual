using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator[] animators;

        [HideInInspector] public int EnemyCount;

        private static readonly int AnimIDDead = Animator.StringToHash("isDead");
        private static readonly int AnimIDShoot = Animator.StringToHash("isShooting");

        private void Awake()
        {
            animators = GetComponentsInChildren<Animator>();
            EnemyCount = animators.Length;
        }


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            Signals.Instance.OnTriggerEnter += WarController;
            Signals.Instance.OnGetPlayerNumber += SetData;
        }

        private void UnSubscribeEvents()
        {
            Signals.Instance.OnTriggerEnter -= WarController;
            Signals.Instance.OnGetPlayerNumber -= SetData;
        }

        private void WarController()
        {
            foreach (var animator in animators)
            {
                animator.SetBool(AnimIDShoot, true);
            }
        }

        private void SetData(int arg0)
        {
            StartCoroutine(TakeDamage(arg0));
        }


        public IEnumerator TakeDamage(int playerNumber)
        {
            //leftEnemyCount = (int)Mathf.Lerp(0, _enemyCount, leftEnemyCount);

            yield return new WaitForSeconds(1f);

            if (playerNumber - EnemyCount > 0)
            {
                for (int i = 0; i < EnemyCount; i++)
                {
                    animators[i].SetBool(AnimIDDead, true);
                }
                //Debug.Log("Player Win");
            }
            else
            {
                for (int i = 0; i < playerNumber; i++)
                {
                    animators[i].SetBool(AnimIDDead, true);
                }
                //Debug.Log("Player Lose");
            }

            //_enemyCount -= playerCount;
            //GetComponent<Collider>().enabled = false;
            //GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.gray);
        }

    }
}