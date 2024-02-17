using DG.Tweening;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public enum ObstacleType
    {
        None,
        RotateY,
        RotateZ,
        MovePosZ,
        MovePosX,
        ChangeScale
    }

    public ObstacleType ObstacleBehaviour;

    [SerializeField] private float speed;
    [SerializeField] private float moveDistance;


    private void Start()
    {
        switch (ObstacleBehaviour)
        {
            case ObstacleType.RotateY:
                transform.DOLocalRotate(new Vector3(0,180,0), 1/speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case ObstacleType.RotateZ:
                transform.DOLocalRotate(new Vector3(0,0,180), 1/speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case ObstacleType.MovePosZ:
                transform.DOLocalMoveZ(transform.position.z + moveDistance, 1/speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case ObstacleType.MovePosX:
                transform.DOLocalMoveX(transform.position.x + moveDistance, 1 / speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case ObstacleType.ChangeScale:
                transform.DOScale(new Vector3(5.5f,1f,1f), 1/speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
        }
    }
    
}
