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
                if (unit.PlayerController.currentElement == ElementType.Default)
                    animName = "KenzenAttackRight";
                else if (unit.PlayerController.currentElement == ElementType.Air)
                    animName = "AirKenzenAttackRight";
                else if (unit.PlayerController.currentElement == ElementType.Earth)
                {
                    animName = "EarthKenzenAttackRight";
                }
                break;
            case Directions.Left:
                animName = "KenzenAttackRight";
                if (unit.PlayerController.currentElement == ElementType.Default)
                    animName = "KenzenAttackRight";
                else if (unit.PlayerController.currentElement == ElementType.Air)
                    animName = "AirKenzenAttackRight";
                else if (unit.PlayerController.currentElement == ElementType.Earth)
                {
                    animName = "EarthKenzenAttackRight";
                }
                break;
            case Directions.Up:
                animName = "KenzenAttackUp";
                if (unit.PlayerController.currentElement == ElementType.Default)
                    animName = "KenzenAttackUp";
                else if (unit.PlayerController.currentElement == ElementType.Air)
                    animName = "AirKenzenAttackUp";
                else if (unit.PlayerController.currentElement == ElementType.Earth)
                {
                    animName = "EarthKenzenAttackRight";
                }
                break;
            case Directions.Down:
                animName = "KenzenAttackDown";
                if (unit.PlayerController.currentElement == ElementType.Default)
                    animName = "KenzenAttackDown";
                else if (unit.PlayerController.currentElement == ElementType.Air)
                    animName = "AirKenzenAttackDown";
                else if (unit.PlayerController.currentElement == ElementType.Earth)
                {
                    animName = "EarthKenzenAttackRight";
                }
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
