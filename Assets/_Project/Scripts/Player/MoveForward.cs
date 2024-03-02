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

    public bool CanMoving;

    private Vector3 _moveDirectionVector;

    #endregion

    private void OnEnable() => SubscribeEvents();

    private void Start()
    {
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
        if (CanMoving) transform.position += _moveDirectionVector * (moveSpeed * Time.deltaTime);
    }
    private void OnDisable() => UnSubscribeEvents();

    public void SubscribeEvents()
    {
        Signals.Instance.OnGameRunning += SetRunningState;
    }

    public void UnSubscribeEvents()
    {
        Signals.Instance.OnGameRunning -= SetRunningState;
    }
    private void SetRunningState() => CanMoving = true;

}
