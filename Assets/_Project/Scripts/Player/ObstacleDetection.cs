using UnityEngine;
using RakibJahan;
public class ObstacleDetection : MonoBehaviour
{
    [SerializeField] private PlayerCrowd playerCrowd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerClone"))
        {
            Debug.Log("Player Hit");

            playerCrowd.Shooters.Remove(other.gameObject.GetComponent<PlayerShooter>());
            //playerCrowd.ArrangeShooters();

            Destroy(other.gameObject);
        }
    }
}
