using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenzenIdleState : State
{
    public KenzenIdleState(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
        : base(owner, stateMachine, animator) { }
    public override void Enter() {
        animator.Play("KenzenIdle");
    } 
    public override void Update() { } 
    public override void Exit() { } 
}
