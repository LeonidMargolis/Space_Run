using UnityEngine;

public class CollectiblesController : MonoBehaviour
{
    private WorldController controller = WorldController.Instance;

    void Start()
    {
        controller.OnCollectibleMovement += TryToDelCollectible;
    }

    private void TryToDelCollectible()
    {
        if (transform.position.z < controller.minZ)
        {
            Destroy(gameObject);
        }

        if (controller.collectiblesCount < 2)
        {
            controller.worldGenerator.CreateCollectible();
        }
    }

    private void OnDestroy()
    {
        controller.OnCollectibleMovement -= TryToDelCollectible;
        controller.collectiblesCount--;
    }
}
