using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RakibJahan
{
    
    public class PlayerCrowd : MonoBehaviour, IRemoveShooter
    {

        [SerializeField] private int crowdSizeForDebug = 5;
        [SerializeField] private int startingCrowdSize = 1;

        [SerializeField] private PlayerShooter shooterPrefab;
        [SerializeField] private List<Transform> spawnPoints = new();

        public List<PlayerShooter> Shooters = new();
        [ContextMenu("Set")]
        public void Debug_ResizeCrowd() => Set(crowdSizeForDebug);

        [SerializeField] private TMP_Text yearText;

        private MoveForward _moveForward;

        private int _year;

        private void Start()
        {
            Set(startingCrowdSize);
            yearText.text = _year.ToString();
        }

        public void AddYearToCrowd(int yearToAdd)
        {
            _year += yearToAdd;
            foreach (var shooter in Shooters)
            {
                shooter.UpdateWeaponYear(yearToAdd);
            }
            yearText.text = _year.ToString();
        }
        public void Set(int amount)
        {
            if (Shooters.Count == amount) return;
            var needToRemove = amount < Shooters.Count;
            var needToAdd = amount > Shooters.Count;
            if (amount != Shooters.Count)
            {
                if (needToRemove) RemoveShooter();
                else if (needToAdd) AddShooter();
            }
        }

        public bool CanAdd()
        {
            return Shooters.Count + 1 <= spawnPoints.Count;
        }

        public bool CanRemove()
        {
            return Shooters.Count - 1 >= 0;
        }
        public void RemoveShooter()
        {
            if (!CanRemove()) StartCoroutine(StopGame());
            var lastShooter = Shooters[^1];
            Shooters.Remove(lastShooter);
            Destroy(lastShooter.gameObject);

            ArrangeShooters();
        }

        public void AddShooter()
        {
            if (!CanAdd()) return;
            var lastShooterIndex = Shooters.Count - 1;
            var position = spawnPoints[lastShooterIndex + 1].position;
            var shooter = Instantiate(shooterPrefab, position, Quaternion.identity, transform);
            Shooters.Add(shooter);

            ArrangeShooters();
        }

        public void ArrangeShooters()
        {
            //Shooters.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));

            for (int i = 0; i < Shooters.Count; i++)
            {
                var newPosition = spawnPoints[i].position;
                Shooters[i].transform.position = newPosition;
            }
        }



        public void RemoveShooterBossFight(int enemyCount)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                var lastShooter = Shooters[^1];
                Shooters.Remove(lastShooter);
                Destroy(lastShooter.gameObject);

                if (Shooters.Count <= 0)
                {
                    //Debug.Log("You Lose");
                    StartCoroutine(StopGame());
                }

            }
        }

        public IEnumerator StopGame()
        {
            _moveForward = transform.parent.GetComponent<MoveForward>();
            _moveForward.MoveSpeed = 0;
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}