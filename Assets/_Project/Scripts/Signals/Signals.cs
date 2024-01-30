using UnityEngine.Events;

public class Signals : MonoSingleton<Signals>
{
    public UnityAction OnTriggerEnter = delegate { };
    public UnityAction<int> OnGetPlayerNumber = delegate { };
    public UnityAction<int> OnGetEnemyNumber = delegate { };


}
