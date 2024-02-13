using UnityEngine;

public class Shooter : MonoBehaviour
{
    public enum ShooterType
    {
        Player, Enemy
    }
    public ShooterType type;

    [SerializeField] private Shootable shootablePrefab;
    [SerializeField] private Transform shootFrom;
    
    private Animator _animator;

    private static readonly int AnimIDShoot = Animator.StringToHash("isShooting");
    private static readonly int AnimIDDeath = Animator.StringToHash("isDead");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() => SubscribeEvents();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Signals.Instance.OnTriggerEnterObstacle?.Invoke(gameObject);
            Signals.Instance.OnChangePlayerNumber?.Invoke(GameManager.Instance.PlayerCrowd.Shooters.Count);
        }
    }

    void OnDisable() => UnSubscribeEvents();

    private void SubscribeEvents()
    {
        Signals.Instance.OnPlayerWin += EndGamePlayerWinController;
        Signals.Instance.OnPlayerLose += EndGamePlayerLoseController;
    }

    private void UnSubscribeEvents()
    {
        Signals.Instance.OnPlayerWin -= EndGamePlayerWinController;
        Signals.Instance.OnPlayerLose -= EndGamePlayerLoseController;
    }


    internal void Shoot()
    {
        Instantiate(shootablePrefab, shootFrom.position, Quaternion.identity).Init();
    }

    private void EndGamePlayerWinController()
    {
        if (type == ShooterType.Player) _animator.SetBool(AnimIDShoot, false);
        else _animator.SetBool(AnimIDDeath, true);
    }
    private void EndGamePlayerLoseController()
    {
        if (type == ShooterType.Player) _animator.SetBool(AnimIDDeath, true);
        else _animator.SetBool(AnimIDShoot, false);
    }
}
