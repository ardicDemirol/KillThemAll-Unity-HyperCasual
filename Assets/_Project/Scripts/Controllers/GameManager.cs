using _Project.Scripts.Enemy;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerCrowd playerCrowd;
    public Enemy enemy;

    public System.Collections.IEnumerator StopGame()
    {
        MoveForward.Instance.MoveSpeed = 0;
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
