using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
    private float animationLength;
    
    public AttackState(PlayerStateMachine unit) : base(unit) { }

    public override void Enter()
    {

        string animName = "";

        switch (DirectionUtils.currentDirection)
        {
            case Directions.Right:
                animName = "KenzenAttackRight";
                break;
            case Directions.Left:
                animName = "KenzenAttackRight";
                break;
            case Directions.Up:
                animName = "KenzenAttackUp";
                break;
            case Directions.Down:
                animName = "KenzenAttackDown";
                break;
        }

        if (!string.IsNullOrEmpty(animName))
        {
            unit.GetAnimator().Play(animName, 0);

            // Pega duração da animação com segurança
            AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
            animationLength = clip != null ? clip.length : 0.5f; // fallback se não encontrar

            // Ativa detecção de inimigos
            unit.FindEnemyOnAttack.FindEnemy();
            unit.StartCoroutine(WaitForAnimation(animationLength));
        }
    }

    public override void Tick() { }

    public override void Exit() { 
        unit.PlayerController.canAttack = true; }

    private IEnumerator WaitForAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        unit.ChangeState<IdleState>();
    }

}
