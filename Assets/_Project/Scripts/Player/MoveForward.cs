using UnityEngine;

public class MoveForward : MonoBehaviour
{
    #region Variables
    public enum Direction
    {
        Forward,
        Backward,
    }
    public enum Type
    {
        Player,
        Bullet
    }

    public Direction MoveDirection;
    public Type ObjectType;


    [SerializeField] private float moveSpeed = 8f;
    [HideInInspector] public float CurrentMoveSpeed;

    private float _normalMoveSpeed;

    private Vector3 _moveDirectionVector;

    #endregion

    private void OnEnable() => SubscribeEvents();

    private void Start()
    {
        CurrentMoveSpeed = moveSpeed;
        _normalMoveSpeed = moveSpeed;

        switch (MoveDirection)
        {
            case Direction.Forward:
                _moveDirectionVector = Vector3.forward;
                break;
            case Direction.Backward:
                _moveDirectionVector = Vector3.back;
                break;
        }
    }
    private void Update()
    {
        transform.position += _moveDirectionVector * (CurrentMoveSpeed * Time.deltaTime);
    }
    private void OnDisable() => UnSubscribeEvents();

    public void SubscribeEvents() 
    {
        Signals.Instance.OnGameRunning += SetRunningSpeed;
    }

    public void UnSubscribeEvents()
    {
        Signals.Instance.OnGameRunning -= SetRunningSpeed;
    }
    private void SetRunningSpeed()
    {
        CurrentMoveSpeed = _normalMoveSpeed;
    }

}
