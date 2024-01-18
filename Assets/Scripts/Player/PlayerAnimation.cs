using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Colliders")]
    [SerializeField] private CapsuleCollider defaultCollider;
    [SerializeField] private CapsuleCollider jumpCollider;
    [SerializeField] private CapsuleCollider slideCollider;
    [Space]
    [SerializeField] private GameObject playerObject;
    private Animator animator;
    private float jumpTime;
    private float slideTime;
    public static PlayerAnimation Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        animator = playerObject.GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Big Jump":
                    jumpTime = clip.length;
                    break;
                case "Running Slide":
                    slideTime = clip.length;
                    break;
            }
        }
    }
    public IEnumerator AnimateJump()
    {
        animator.SetBool("IsJumping", true);
        defaultCollider.enabled = false;
        jumpCollider.enabled = true;
        yield return new WaitForSecondsRealtime(jumpTime);
        animator.SetBool("IsJumping", false);
        defaultCollider.enabled = true;
        jumpCollider.enabled = false;
    }

    public IEnumerator AnimateSlide()
    {
        animator.SetBool("IsSliding", true);
        defaultCollider.enabled = false;
        slideCollider.enabled = true;
        yield return new WaitForSecondsRealtime(slideTime);
        animator.SetBool("IsSliding", false);
        defaultCollider.enabled = true;
        slideCollider.enabled = false;
    }
    public void AnimateRun()
    {
        animator.SetBool("IsRunning", true);
    }
    public void StopJumping()
    {
        animator.SetBool("IsJumping", false);
    }   
    public void StopSliding()
    {
        animator.SetBool("IsSliding", false);
    }
    public bool IsJumping()
    {
        return animator.GetBool("IsJumping");
    }    
    public bool IsSliding()
    {
        return animator.GetBool("IsSliding");
    }
}
