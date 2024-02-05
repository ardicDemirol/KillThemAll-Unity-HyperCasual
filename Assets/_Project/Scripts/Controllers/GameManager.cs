using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerCrowd playerCrowd;
    public Enemy enemy;
    public MoveForward moveForward;

    public System.Collections.IEnumerator StopGame()
    {
        moveForward.CurrentMoveSpeed = 0;
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    
}
