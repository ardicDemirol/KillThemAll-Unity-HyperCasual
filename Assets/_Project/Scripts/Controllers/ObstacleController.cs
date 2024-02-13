using DG.Tweening;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] Transform target;
    public enum ObstacleType
    {
        None,
        MovePosY,
        MovePosZ,
        MovePosX,
        ChangeScale
    }

    public ObstacleType obstacleType;

    float firstPosX;
    private void Start()
    {
        firstPosX = transform.position.x;

        switch (obstacleType)
        {
            case ObstacleType.MovePosY:
                transform.DOLocalMoveY(transform.position.y + 10f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case ObstacleType.MovePosZ:
                transform.DOLocalMoveZ(transform.position.z + 5f,1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case ObstacleType.MovePosX:
                transform.DOLocalMoveX(transform.position.x + 5f,1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
                break;
            case ObstacleType.ChangeScale:
                transform.DOScale(new Vector3(2, 2, 2), 1f).SetLoops(-1, LoopType.Yoyo);
                break;
        }
    }
    void Update()
    {
        //switch (obstacleType)
        //{
        //    case ObstacleType.MovePosY:
        //        transform.DOLocalMove(new Vector3(transform.position.x,10f,transform.position.z),1f).SetLoops(5,LoopType.Yoyo);
        //    case ObstacleType.MovePosZ:
        //        transform.DOMoveZ(10,1).SetLoops(-1, LoopType.Yoyo);
        //    case ObstacleType.MovePosX:
        //        transform.DOMove(new Vector3(firstPosX + 5, transform.position.y, transform.position.z), 1f).SetLoops(5, LoopType.Yoyo);
        //    case ObstacleType.ChangeScale:
        //        transform.DOScale(new Vector3(2, 2, 2), 1f).SetLoops(-1, LoopType.Yoyo);
        //}

    }
}
