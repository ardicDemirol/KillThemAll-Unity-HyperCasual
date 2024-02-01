using Modifiers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Modifier"))
        {
            var modifier = other.GetComponent<ModifierBase>();
            if (modifier)
            {
                modifier.Modify(this);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Signals.Instance.OnTriggerEnterEnemy?.Invoke();
            Signals.Instance.OnGetPlayerNumber?.Invoke(GameManager.Instance.playerCrowd.Shooters.Count);
            Signals.Instance.OnGetEnemyNumber?.Invoke(GameManager.Instance.enemy.EnemyCount);
        }

        Signals.Instance.OnChangePlayerNumber?.Invoke(GameManager.Instance.playerCrowd.Shooters.Count);
    }
}
