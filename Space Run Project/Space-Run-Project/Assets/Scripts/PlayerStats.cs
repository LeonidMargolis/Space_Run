using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int playerScore = 0;
    private int difficultyUpCondition = 500;

    private void Awake()
    {
        if (PlayerStats.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        PlayerStats.Instance = this;
    }
    private void OnDestroy()
    {
        PlayerStats.Instance = null;
    }

    public int ScoreAdd(int amount)
    {
        playerScore += amount;
        return playerScore;

    }

    private void Update()
    {
        IncreaseDifficulty();
    }

    private void IncreaseDifficulty()
    {
        if (playerScore >= difficultyUpCondition)
        {
            WorldController.Instance.speed += 1f;
            difficultyUpCondition += difficultyUpCondition;
        }
    }
}
