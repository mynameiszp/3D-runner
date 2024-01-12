using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    private Animator animator;
    public static PlayerAnimation Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        animator = playerObject.GetComponent<Animator>();
    }
    public IEnumerator AnimateJump()
    {
        animator.SetBool("IsJumping", true);
        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        animator.SetBool("IsJumping", false);
    }

    public IEnumerator AnimateSlide()
    {
        animator.SetBool("IsSliding", true);
        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        animator.SetBool("IsSliding", false);
    }
}
