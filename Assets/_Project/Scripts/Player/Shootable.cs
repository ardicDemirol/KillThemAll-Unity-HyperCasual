using UnityEngine;

public class Shootable : MonoBehaviour
{
    [SerializeField] private float shootableSurviveTime = 2f;
    public void Init() => Destroy(gameObject, shootableSurviveTime);
}
