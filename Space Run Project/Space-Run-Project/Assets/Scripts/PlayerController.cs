using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;

    [SerializeField] private float jumpForce = 10.0f;
    [SerializeField] private float gravityForce = 5.0f;
    
    public float rollDistance = 3.0f;

    private float animRollTime;
    private float animRollSpeed;
    private float jumpSpeed;

    private int currentPath = 2;
    private float currentDistance = 0f;
    private float currentDirection = 0f;

    private bool isAlive;

    public static PlayerController Instance;

    private void Awake()
    {
        if (PlayerController.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        PlayerController.Instance = this;
    }
    private void OnDestroy()
    {
        PlayerController.Instance = null;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        animRollTime = GetAnimRollTime(animator.runtimeAnimatorController.animationClips);
        animRollSpeed = rollDistance / animRollTime;

        StartCoroutine(OnPlayerMovementCheckCoroutine());
    }

    private float GetAnimRollTime(AnimationClip[] clips)
    {
        foreach (var clip in clips)
        {
            if (clip.name.Equals("Left Roll") || clip.name.Equals("Right Roll"))
                return clip.length;
        }
        Debug.LogWarning("Анимация переката не найдена!");
        return 0f;
    }

    
    public void RunStart()
    {
        animator.SetTrigger("Run");
    }

    void Update()
    {
        Gravity();
        isAlive = SceneEngine.Instance.isGameRunning;
    }

    private void Gravity()
    {
        controller.Move(Vector3.down * gravityForce * Time.deltaTime);
    }

    private IEnumerator OnPlayerMovementCheckCoroutine()
    {
        while (true)
        {
            if (isAlive)
            {
                float directionHor = Input.GetAxisRaw("Horizontal");
                float directionVer = Input.GetAxisRaw("Vertical");

                if ((directionHor > 0 && currentPath < 3) || (directionHor < 0 && currentPath > 1))
                {
                    currentDirection = directionHor;
                    currentDistance = rollDistance;
                    animator.SetTrigger(directionHor < 0 ? "Left" : "Right");

                    yield return StartCoroutine(OnPlayerMoveCoroutine());
                }

                if (directionVer == 1)
                {
                    jumpSpeed = jumpForce;
                    animator.SetBool("isJumping", true);

                    yield return StartCoroutine(OnPlayerJumpCoroutine());
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator OnPlayerJumpCoroutine()
    {
        while (true)
        {
            jumpSpeed -= Time.deltaTime * gravityForce;
            controller.Move(jumpSpeed * Time.deltaTime * Vector3.up);

            if (controller.isGrounded)
            {
                animator.SetBool("isJumping", false);
                break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator OnPlayerMoveCoroutine()
    {
        while (true)
        {
            if (currentDistance <= 0)
            {
                currentPath = currentDirection < 0 ? currentPath - 1 : currentPath + 1;
                break;
            }

            if (!isAlive)
            {
                break;
            }

            float tmpDistance = Time.deltaTime * animRollSpeed;

            controller.Move(currentDirection * tmpDistance * Vector3.right);
            currentDistance -= tmpDistance;

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Danger"))
        {
            PlayerDeathEvent();
        }
    }

    private void PlayerDeathEvent()
    {
        SceneEngine.Instance.InitGameEnd();
        animator.SetTrigger("Death");
    }
}
