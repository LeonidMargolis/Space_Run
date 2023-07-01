using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    
    public GameObject[] gameUIObjects;
    private bool isActive = true;
    private TMP_Text tmp_text;

    private void Awake()
    {
        if (GameUI.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        GameUI.instance = this;
    }

    private void OnDestroy()
    {
        GameUI.instance = null;
    }

    private void Start()
    {
        tmp_text = transform.Find("Score").GetComponent<TMP_Text>();
        ChangeGameUIState();
        ScoreUpdateText(0);
    }

    private void Update()
    {
        ScoreUpdateText(PlayerStats.Instance.playerScore);
    }

    public void ChangeGameUIState()
    {
        isActive = !isActive;
        foreach (GameObject obj in gameUIObjects)
        {
            obj.SetActive(isActive);
        }
    }

    public void ScoreUpdateText(int score)
    {
        tmp_text.text = $"Ñ÷¸ò: {score}";
    }
}
