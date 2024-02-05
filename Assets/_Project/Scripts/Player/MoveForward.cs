using UnityEngine;

public class MoveForward : MonoBehaviour
{
    #region Variables
    [SerializeField] private float moveSpeed = 8f;
    [HideInInspector] public float CurrentMoveSpeed = 0f;
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

   
    private float _normalMoveSpeed;
    private float _speed;

    private Vector3 moveDirectionVector;


    #endregion

    private void OnEnable() => SubscribeEvents();

    private void Start()
    {
        if(ObjectType == Type.Player)
        {
            _normalMoveSpeed = moveSpeed;
        }
        else
        {
            CurrentMoveSpeed = moveSpeed;   
        }

        switch (MoveDirection)
        {
            case Direction.Forward:
                moveDirectionVector = Vector3.forward;
                break;
            case Direction.Backward:
                moveDirectionVector = Vector3.back;
                break;
        }
    }
    private void Update()
    {
        transform.position += moveDirectionVector * (CurrentMoveSpeed * Time.deltaTime);
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
