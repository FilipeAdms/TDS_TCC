using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTransformationState : State
{
    private float animationLength; // Dura��o da anima��o
    public AirTransformationState(PlayerStateMachine unit) : base(unit)
    {
    }

    public override void Enter()
    {
        Debug.Log("Estado do Ar");
        string animName = "KenzenTransformationAir"; // Nome da anima��o a ser tocada

        SetAirStats();
        Debug.Log("batata" + !string.IsNullOrEmpty(animName));

        if (!string.IsNullOrEmpty(animName))
        {
            Debug.Log("Tocando a anima��o");
            unit.GetAnimator().Play(animName, 0);

            // Pega dura��o da anima��o com seguran�a
            AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
            animationLength = clip != null ? clip.length : 0.5f; // fallback se n�o encontrar

            // Ativa detec��o de inimigos
            unit.StartCoroutine(WaitForAnimation(animationLength));
        }
    }
    public override void Tick()
    {

    }
    public override void Exit()
    {
    }

    private void SetAirStats()
    {
        float tempSpeed = unit.Status.currentMoveSpeed * 0.25f;
        float tempDamage = unit.Status.currentAttackDamage * 0.1f;

        unit.Status.ModifyCurrentValue(AttributeType.currentMoveSpeed, tempSpeed); // Aumenta a velocidade em 25%
        unit.Status.ModifyCurrentValue(AttributeType.currentAttackDamage, -tempDamage); // Diminui o dano em 10%
    }
    public IEnumerator WaitForAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        unit.PlayerSkillController.canAct = true;
        unit.ChangeState<IdleState>();
    }
}
