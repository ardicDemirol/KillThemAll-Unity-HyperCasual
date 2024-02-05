using UnityEngine;
using UnityEngine.Events;

public class Signals : MonoSingleton<Signals>
{
    public UnityAction OnTriggerEnter = delegate { };
    public UnityAction<GameObject> OnTriggerEnterObstacle = delegate { };
    public UnityAction<int> OnGetPlayerNumber = delegate { };
    public UnityAction<int> OnGetEnemyNumber = delegate { };
    public UnityAction<int> OnChangePlayerNumber = delegate { };
    public UnityAction<int> OnChangeEnemyNumber = delegate { };
    public UnityAction OnPlayDieAnimation = delegate { };
    public UnityAction OnPlayerWin = delegate { };
    public UnityAction OnPlayerLose = delegate { };
    public UnityAction OnGameRunning = delegate { };
}
