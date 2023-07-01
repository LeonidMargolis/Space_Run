using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PlatformController : MonoBehaviour
{
    public Transform endPoint;
    
    void Start()
    {
        WorldController.Instance.OnPlatformMovememnt += TryToDelAndAddPlatform;
    }

    private void TryToDelAndAddPlatform()
    {
        if(transform.position.z < WorldController.Instance.minZ)
        {
            WorldController.Instance.worldGenerator.CreatePlatform();
            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        WorldController.Instance.OnPlatformMovememnt -= TryToDelAndAddPlatform;
    }
}
