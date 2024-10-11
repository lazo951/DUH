using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTest : MonoBehaviour
{
    Animator animator;
    public string paraName;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu("Play Anim")]
    public void PlayAnim()
    {
        animator.SetTrigger(paraName);
    }
}
