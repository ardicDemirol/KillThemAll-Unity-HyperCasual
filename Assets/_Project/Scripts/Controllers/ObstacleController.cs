using UnityEngine;
public class ObstacleController : MonoBehaviour
{
    [SerializeField] private PlayerCrowd playerCrowd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerClone"))
        {
            playerCrowd.Shooters.Remove(other.gameObject.GetComponent<Shooter>());
            //playerCrowd.ArrangeShooters();
            Destroy(other.gameObject);
        }
    }
}
