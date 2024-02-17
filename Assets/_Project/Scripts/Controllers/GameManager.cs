using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
    public PlayerCrowd PlayerCrowd;
    public Enemy Enemy;
    public MoveForward MoveForward;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    private short _currentSceneIndex;
    private bool _isPaused;
    private static readonly WaitForSeconds _waitForTwoSeconds = new(2f);

    private void OnEnable() => SubscribeEvents();

    private void OnDisable() => UnSubscribeEvents();

    private void SubscribeEvents()
    {
        Signals.Instance.OnGameRunning += CanvasController;
        Signals.Instance.OnPlayerWin += WinController;
        Signals.Instance.OnPlayerLose += LoseController;
    }

    private void UnSubscribeEvents()
    {
        Signals.Instance.OnGameRunning -= CanvasController;
        Signals.Instance.OnPlayerWin -= WinController;
        Signals.Instance.OnPlayerLose -= LoseController;
    }

    internal IEnumerator EndGame()
    {
        MoveForward.CurrentMoveSpeed = 0;
        yield return _waitForTwoSeconds;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadNextLevel()
    {
        yield return _waitForTwoSeconds;

        _currentSceneIndex = (short)SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (_currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void WinController() => StartCoroutine(LoadNextLevel());
    private void LoseController() => StartCoroutine(EndGame());

    private void CanvasController()
    {
        startPanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void TogglePause()
    {
        pauseButton.SetActive(_isPaused);
        pausePanel.SetActive(!_isPaused);
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0f : 1f; 
    }
}
