using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
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
        Debug.Log(jumpTime);
        yield return new WaitForSecondsRealtime(jumpTime);
        animator.SetBool("IsJumping", false);
    }

    public IEnumerator AnimateSlide()
    {
        animator.SetBool("IsSliding", true);
        Debug.Log(slideTime);
        yield return new WaitForSecondsRealtime(slideTime);
        animator.SetBool("IsSliding", false);
    }
    public void AnimateRun()
    {
        animator.SetBool("IsRunning", true);
    }
    public void StopJumping()
    {
        //StopCoroutine(AnimateJump());
        animator.SetBool("IsJumping", false);
    }   
    public void StopSliding()
    {
        //StopCoroutine(AnimateSlide());
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
