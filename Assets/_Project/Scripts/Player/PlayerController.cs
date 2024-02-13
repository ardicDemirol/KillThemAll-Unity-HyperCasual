using Modifiers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _isPlayerInTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Modifier") && !_isPlayerInTrigger)
        {
            _isPlayerInTrigger = true;
            var modifier = other.GetComponent<ModifierBase>();
            if (modifier)
            {
                modifier.Modify(this);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Signals.Instance.OnTriggerEnter?.Invoke();
            Signals.Instance.OnGetPlayerNumber?.Invoke(GameManager.Instance.PlayerCrowd.Shooters.Count);
            Signals.Instance.OnGetEnemyNumber?.Invoke(GameManager.Instance.Enemy.EnemyCount);
        }

        Signals.Instance.OnChangePlayerNumber?.Invoke(GameManager.Instance.PlayerCrowd.Shooters.Count);
    }

    private void OnTriggerExit(Collider other) => _isPlayerInTrigger = false;
}
