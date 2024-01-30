using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private int damagePerShootable = 100;
    [SerializeField] private Shootable shootablePrefab;
    [SerializeField] private Transform shootFrom;

    public void Shoot()
    {
        Instantiate(shootablePrefab, shootFrom.position, Quaternion.identity).Init(damagePerShootable);
    }
}
