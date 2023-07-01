using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Gem : MonoBehaviour
{
    [SerializeField] private readonly int gemScore = 100;
    [SerializeField] private readonly float rotationSpeed = 1.0f;

    private ParticleController _controller;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _controller = transform.Find("Collect Particle").GetComponent<ParticleController>();
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _meshRenderer.enabled = false;
            _controller.ParticlePlay();
            SceneEngine.Instance.CollectGem(gemScore);
        }
    }
}
