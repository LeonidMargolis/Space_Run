using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEngine : MonoBehaviour
{
    private const float StartingWorldSpeed = 5.0f;
    private const short ZeroWorldSpeed = 0;

    public bool isGameRunning = false;

    private int difficultyUpCondition = 500;

    public static SceneEngine Instance;

    private void Awake()
    {
        if(SceneEngine.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        SceneEngine.Instance = this;
    }
    private void OnDestroy()
    {
        SceneEngine.Instance = null;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        IncreaseDifficulty();
    }
    public void InitGameStart()
    {
        isGameRunning = true;
        WorldController.Instance.speed = StartingWorldSpeed;
        PlayerController.Instance.RunStart();
        MainMenu.Instance.ChangeMainMenuState();
        GameUI.instance.ChangeGameUIState();
        StartCoroutine(OnScoreIncrement());
    }

    public void InitGamePauseOrContinue()
    {
        isGameRunning = !isGameRunning;

        if (!isGameRunning)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }

        PauseMenu.Instance.ChangePauseMenuState();
        GameUI.instance.ChangeGameUIState();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void InitGameEnd()
    {
        isGameRunning = false;
        StopCoroutine(OnScoreIncrement());
        WorldController.Instance.speed = ZeroWorldSpeed;
        GameUI.instance.ChangeGameUIState();
        DeathMenu.Instance.ChangeDeathMenuState();
        SoundManager.Instance.StopMainThemeSound();
        SoundManager.Instance.PlayHitSound();
    }

    private void IncreaseDifficulty()
    {
        if (PlayerStats.Instance.playerScore >= difficultyUpCondition)
        {
            WorldController.Instance.speed += 1.0f;
            difficultyUpCondition += difficultyUpCondition;
        }
    }

    public void CollectGem(int score)
    {
        SoundManager.Instance.PlayCollectSound();
        PlayerStats.Instance.ScoreAdd(score);
    }

    private IEnumerator OnScoreIncrement()
    {
        while (true)
        {
            PlayerStats.Instance.ScoreAdd(1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
