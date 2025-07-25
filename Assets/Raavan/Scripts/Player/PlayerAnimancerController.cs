using UnityEngine;
using Animancer;
using StarterAssets; // ✅ Required to access ThirdPersonController

[RequireComponent(typeof(AnimancerComponent))]
public class PlayerAnimancerController : MonoBehaviour
{
    [Header("Animation Clips")]
    public AnimationClip mainIdleClip; // Default idle (looping)
    public AnimationClip[] breakIdleClips; // Random idles to trigger later
    public AnimationClip walkClip;
    public AnimationClip runClip;
    public AnimationClip crouchIdleClip;
    public AnimationClip crouchWalkClip;


    private AnimancerComponent animancer;
    private ThirdPersonController controller;

    [Header("Transition Settings")]
    public float walkSpeedThreshold = 0.1f;
    public float runSpeedThreshold = 2.5f;
    public float fadeDuration = 0.2f;
    public float idleBreakMinDelay = 5f;
    public float idleBreakMaxDelay = 10f;

    private float idleBreakTimer;
    private bool isInBreakIdle = false;

    private bool wasMovingLastFrame = false;

    private void Awake()
    {
        animancer = GetComponent<AnimancerComponent>();
        controller = GetComponent<ThirdPersonController>();
    }

    private void Start()
{
    animancer.Play(mainIdleClip, 0); // No fade needed here
}


    void Update()
    {
        float currentSpeed = controller.CurrentSpeed;
        bool isMoving = currentSpeed >= walkSpeedThreshold;

        if (controller.Grounded)
{
    if (controller.isCrouching)
{
    if (isMoving)
    {
        animancer.Play(crouchWalkClip, fadeDuration);
    }
    else
    {
        animancer.Play(crouchIdleClip, fadeDuration);
    }
}

    else
    {
        if (!isMoving && (wasMovingLastFrame || animancer.States.Current.Clip == crouchIdleClip))
        {
            animancer.Play(mainIdleClip, fadeDuration);
        }

        if (!isMoving && animancer.States.Current == null)
        {
            animancer.Play(mainIdleClip, fadeDuration);
        }

        if (currentSpeed >= runSpeedThreshold)
        {
            animancer.Play(runClip, fadeDuration);
        }
        else if (currentSpeed >= walkSpeedThreshold)
        {
            animancer.Play(walkClip, fadeDuration);
        }
    }
}


        wasMovingLastFrame = isMoving;
    }

    private void PlayBreakIdle()
    {
        if (breakIdleClips.Length == 0)
            return;

        int index = Random.Range(0, breakIdleClips.Length);
        animancer.Play(breakIdleClips[index], fadeDuration);
        isInBreakIdle = true;
    }
}
