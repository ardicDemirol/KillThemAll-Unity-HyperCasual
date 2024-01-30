using UnityEngine;

namespace RakibJahan
{
    public class MoveForward : MonoBehaviour
    {
        public float MoveSpeed = 10f;

        private void Update()
        {
            transform.position += Vector3.forward * (MoveSpeed * Time.deltaTime);
        }
    }
}