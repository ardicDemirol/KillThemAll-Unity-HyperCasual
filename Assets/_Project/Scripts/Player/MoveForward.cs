using UnityEngine;

public class MoveForward : MonoSingleton<MoveForward>
{
    #region Variables
    public float MoveSpeed = 10f;
    public enum MoveDirection
    {
        Forward,
        Backward,
        Left,
        Right
    }

    private Vector3 moveDirectionVector;
    public MoveDirection _moveDirection;

    #endregion

    private void Start()
    {
        switch (_moveDirection)
        {
            case MoveDirection.Forward:
                moveDirectionVector = Vector3.forward;
                break;
            case MoveDirection.Backward:
                moveDirectionVector = Vector3.back;
                break;
            case MoveDirection.Left:
                moveDirectionVector = Vector3.left;
                break;
            case MoveDirection.Right:
                moveDirectionVector = Vector3.right;
                break;
        }
    }

    private void Update()
    {
        transform.position += moveDirectionVector * (MoveSpeed * Time.deltaTime);
    }
}
