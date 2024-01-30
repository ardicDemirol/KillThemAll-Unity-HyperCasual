using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCrowd : MonoBehaviour
{
    #region Variables
    public List<Shooter> Shooters = new();

    [SerializeField] private int crowdSizeForDebug = 5;
    [SerializeField] private int startingCrowdSize = 1;

    [SerializeField] private Shooter shooterPrefab;
    [SerializeField] private List<Transform> spawnPoints = new();

    [SerializeField] private Animator[] animators;

    [SerializeField] private TMP_Text crowdSizeText;

    private static readonly int AnimIDDead = Animator.StringToHash("isDead");
    private static readonly int AnimIDShoot = Animator.StringToHash("isShooting");

    #endregion


    private void OnEnable() => SubscribeEvents();


    private void Start()
    {
        Set(startingCrowdSize);
        crowdSizeText.text = Shooters.Count.ToString();
    }

    private void OnDisable() => UnSubscribeEvents();


    [ContextMenu("Set")]
    public void Debug_ResizeCrowd() => Set(crowdSizeForDebug);

    private void SubscribeEvents()
    {
        Signals.Instance.OnTriggerEnter += WarController;
        Signals.Instance.OnGetEnemyNumber += SetData;
    }
    private void UnSubscribeEvents()
    {
        Signals.Instance.OnTriggerEnter -= WarController;
        Signals.Instance.OnGetEnemyNumber -= SetData;

    }

    private void SetData(int arg0)
    {
        StartCoroutine(RemoveShooterFight(arg0));
    }

    private void WarController()
    {
        MoveForward.Instance.MoveSpeed = 0;
        animators = GetComponentsInChildren<Animator>();
        foreach (var animator in animators)
        {
            animator.SetBool(AnimIDShoot, true);
        }
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
        if (!CanRemove())
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.StopGame());
            Debug.Log(1);
        }
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


    private IEnumerator RemoveShooterFight(int enemyNumber)
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < enemyNumber; i++)
        {
            var lastShooter = Shooters[^1];
            Shooters.Remove(lastShooter);
            Destroy(lastShooter.gameObject);

            if (Shooters.Count <= 0)
            {
                GameManager.Instance.StartCoroutine(GameManager.Instance.StopGame());
            }
        }
    }
   

}
