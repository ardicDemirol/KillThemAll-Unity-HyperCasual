using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCrowd : MonoBehaviour
{
    #region Variables
    public List<Shooter> Shooters = new();

    [SerializeField] private int startingCrowdSize = 1;
    [SerializeField] private Shooter shooterPrefab;
    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private Animator[] animators;
    [SerializeField] private TMP_Text crowdSizeText;

    private static readonly WaitForSeconds _waitForOneSeconds = new(1f);

    private static readonly int AnimIDShoot = Animator.StringToHash("isShooting");
    private static readonly int AnimIDDeath = Animator.StringToHash("isDead");

    private Shooter willBeRemoveShooter;
    #endregion

    private void Awake()
    {
        Set(startingCrowdSize);
        SetPlayerNumber(Shooters.Count);
    }

    private void OnEnable() => SubscribeEvents();

    private void OnDisable() => UnSubscribeEvents();

    private void SubscribeEvents()
    {
        Signals.Instance.OnTriggerEnter += WarController;
        Signals.Instance.OnTriggerEnterObstacle += RemoveShooter;
        Signals.Instance.OnGetEnemyNumber += SetData;
        Signals.Instance.OnChangePlayerNumber += SetPlayerNumber;

    }
    private void UnSubscribeEvents()
    {
        Signals.Instance.OnTriggerEnter -= WarController;
        Signals.Instance.OnTriggerEnterObstacle -= RemoveShooter;
        Signals.Instance.OnGetEnemyNumber -= SetData;
        Signals.Instance.OnChangePlayerNumber -= SetPlayerNumber;
    }

    private void SetPlayerNumber(int playerCount)
    {
        crowdSizeText.text = playerCount.ToString();
    }

    private void SetData(int arg0)
    {
        StartCoroutine(RemoveShooterFight(arg0));
    }

    private void WarController()
    {
        GameManager.Instance.MoveForward.CanMoving = false; 
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
        while (amount != Shooters.Count)
        {
            if (needToRemove) RemoveShooter(null);
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
    public void RemoveShooter(GameObject obj)
    {
        if(Shooters.Count == 0) return;

        willBeRemoveShooter = Shooters[^1];

        if (obj) willBeRemoveShooter = obj.GetComponent<Shooter>();

        willBeRemoveShooter.GetComponent<Animator>().SetBool(AnimIDDeath, true);
        willBeRemoveShooter.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.gray);

        willBeRemoveShooter.transform.SetParent(null);

        Shooters.Remove(willBeRemoveShooter);

        if (!CanRemove())
        {
            Signals.Instance.OnPlayerLose?.Invoke();
            Signals.Instance.OnTriggerEnter?.Invoke();
        }

        StartCoroutine(ArrangeShooters());
    }

    public void AddShooter()
    {
        if (!CanAdd()) return;
        var lastShooterIndex = Shooters.Count - 1;
        var position = spawnPoints[lastShooterIndex + 1].position;
        var shooter = Instantiate(shooterPrefab, position, Quaternion.identity, transform);
        Shooters.Add(shooter);

        StartCoroutine(ArrangeShooters());
    }
    public IEnumerator ArrangeShooters()
    {
        yield return _waitForOneSeconds;
        for (int i = 0; i < Shooters.Count; i++)
        {
            var newPosition = spawnPoints[i].position;
            Shooters[i].transform.position = newPosition;
        }
    }

    private IEnumerator RemoveShooterFight(int enemyNumber)
    {
        yield return _waitForOneSeconds;

        for (int i = 0; i < enemyNumber; i++)
        {
            if (Shooters.Count == 0) break;

            var lastShooter = Shooters[^1];
            Shooters.Remove(lastShooter);

            lastShooter.GetComponent<Animator>().SetBool(AnimIDDeath, true);
            lastShooter.transform.SetParent(null);
            lastShooter.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);

            if (Shooters.Count <= 0)
            {
                Signals.Instance.OnPlayerLose?.Invoke();
                Signals.Instance.OnTriggerEnter?.Invoke();
            }
        }

        Signals.Instance.OnChangePlayerNumber?.Invoke(Shooters.Count);
    }


}
