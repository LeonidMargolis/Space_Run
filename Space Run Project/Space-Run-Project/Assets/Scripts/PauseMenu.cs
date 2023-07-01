using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] pauseMenuObjects;
    private bool isActive = true;

    public static PauseMenu Instance;

    private void Awake()
    {
        if (PauseMenu.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        PauseMenu.Instance = this;
    }
    private void OnDestroy()
    {
        PauseMenu.Instance = null;
    }

    private void Start()
    {
        ChangePauseMenuState();
    }

    public void ChangePauseMenuState()
    {
        isActive = !isActive;
        foreach (GameObject obj in pauseMenuObjects)
        {
            obj.SetActive(isActive);
        }
    }
}
