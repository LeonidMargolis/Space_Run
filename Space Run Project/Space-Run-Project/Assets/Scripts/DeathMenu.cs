using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public GameObject[] deathMenuObjects;
    private bool isActive = true;

    public static DeathMenu Instance;

    private void Awake()
    {
        if (DeathMenu.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DeathMenu.Instance = this;
    }
    private void OnDestroy()
    {
        DeathMenu.Instance = null;
    }

    private void Start()
    {
        ChangeDeathMenuState();
    }

    public void ChangeDeathMenuState()
    {
        isActive = !isActive;
        foreach (GameObject obj in deathMenuObjects)
        {
            obj.SetActive(isActive);
        }
    }
}
