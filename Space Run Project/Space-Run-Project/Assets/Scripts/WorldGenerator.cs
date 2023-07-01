using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private float collectibleDistFromPlatformByY = 1.0f;

    private float pathInterDist;
    public GameObject[] collectibleObjects;

    public GameObject[] freePlatforms;
    public GameObject[] obstaclePlatforms;
    public Transform platformContainer;

    private bool isObstacle;

    private Transform lastPlatform = null;

    void Start()
    {
        pathInterDist = PlayerController.Instance.rollDistance;

        Init();
    }

    public void Init()
    {
        CreateFreePlatform();
        CreateFreePlatform();
        for (int i = 0; i < 12; i++)
        {
            CreateCollectible();
            CreatePlatform();
        }
    }

    public void CreatePlatform()
    {
        if (isObstacle)
        {
            CreateFreePlatform();
        }
        else
        {
            CreateObstaclePlatform();
        }
    }

    public void CreateCollectible()
    {
        if (!isObstacle)
        {
            int index = Random.Range(0, collectibleObjects.Length);
            int path = Random.Range(1, 4);

            float xPos;
            float yPos = lastPlatform.position.y + collectibleDistFromPlatformByY;
            float zPos = lastPlatform.GetComponent<MeshRenderer>().bounds.center.z;

            switch (path)
            {
                case 1:
                    xPos = lastPlatform.position.x - pathInterDist;
                    break;
                case 2:
                    xPos = lastPlatform.position.x;
                    break;
                case 3:
                    xPos = lastPlatform.position.x + pathInterDist;
                    break;
                default:
                    xPos = 0f;
                    Debug.LogWarning("Ошибка X позиции для collectibles");
                    break;
            }
            Vector3 pos = new Vector3(xPos, yPos, zPos);

            Instantiate(collectibleObjects[index], pos, Quaternion.identity, platformContainer);
            WorldController.Instance.collectiblesCount++;
        }
    }

    private bool DecorIsOnTheRight()
    {
        return Random.value >= 0.5f;
    }

    private void CreateFreePlatform()
    {
        Vector3 pos = (lastPlatform == null) ?
            platformContainer.position : lastPlatform.GetComponent<PlatformController>().endPoint.position;

        int index = Random.Range(0, freePlatforms.Length);
        GameObject res = Instantiate(freePlatforms[index], pos, Quaternion.identity, platformContainer);
        lastPlatform = res.transform;

        isObstacle = false;
    }

    private void CreateObstaclePlatform()
    {
        Vector3 pos = (lastPlatform == null) ?
            platformContainer.position : lastPlatform.GetComponent<PlatformController>().endPoint.position;

        int index = Random.Range(0, obstaclePlatforms.Length);
        GameObject res = Instantiate(obstaclePlatforms[index], pos, Quaternion.identity, platformContainer);
        lastPlatform = res.transform;

        isObstacle = true;
    }
}
