using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALK = "isWalk";

    [SerializeField] private Player control;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALK, control.IsWalk());
    }
}
