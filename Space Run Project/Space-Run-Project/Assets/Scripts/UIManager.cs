using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isMainMenuActive = true;
    public void GameStart()
    {
        isMainMenuActive = false;
        SceneEngine.Instance.InitGameStart();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GamePauseOrContinue()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneEngine.Instance.isGameRunning)
        {
            SceneEngine.Instance.InitGamePauseOrContinue();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !SceneEngine.Instance.isGameRunning)
        {
            SceneEngine.Instance.InitGamePauseOrContinue();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (!isMainMenuActive)
        {
            GamePauseOrContinue();
        }
    }
}
