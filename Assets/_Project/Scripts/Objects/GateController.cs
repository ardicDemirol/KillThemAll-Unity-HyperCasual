using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] private PlayerCrowd playerCrowd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerClone") || other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.StopGame());
        }
    }
}
