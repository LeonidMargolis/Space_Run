using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] mainMenuObjects;
    private bool isActive = true;

    public static MainMenu Instance;

    private void Awake()
    {
        if (MainMenu.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        MainMenu.Instance = this;
    }
    private void OnDestroy()
    {
        MainMenu.Instance = null;
    }

    public void ChangeMainMenuState()
    {
        isActive = !isActive;
        foreach(GameObject obj in mainMenuObjects)
        {
            obj.SetActive(isActive);
        }
    }


}
