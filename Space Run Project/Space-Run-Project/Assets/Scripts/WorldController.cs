using System.Collections;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float speed;
    public float minZ = -15.0f;
    public float collectiblesCount = 0;

    public delegate void TryToDelAndAddPlatform();
    public event TryToDelAndAddPlatform OnPlatformMovememnt;
    public delegate void TryToDelCollectible();
    public event TryToDelCollectible OnCollectibleMovement;

    public WorldGenerator worldGenerator;

    public static WorldController Instance;

    private void Awake()
    {
        if (WorldController.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        WorldController.Instance = this;
    }
    private void OnDestroy()
    {
        WorldController.Instance = null;
    }

    void Start()
    {
        StartCoroutine(OnPlatformMovementCoroutine());
        StartCoroutine(OnCollectibleMovementCoroutine());
    }

    void Update()
    {
        transform.position -= speed * Time.deltaTime * Vector3.forward;
    }

    IEnumerator OnPlatformMovementCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            OnPlatformMovememnt?.Invoke();
        }
    }

    IEnumerator OnCollectibleMovementCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            OnCollectibleMovement?.Invoke();

            if(collectiblesCount <= 0)
            {
                worldGenerator.CreateCollectible();
            }
        }
    }
}
