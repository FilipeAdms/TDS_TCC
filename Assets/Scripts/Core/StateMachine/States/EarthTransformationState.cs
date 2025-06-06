using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTransformationState : State
{
    private float animationLength; // Duração da animação
    public EarthTransformationState(PlayerStateMachine unit) : base(unit)
    {
    }

    public override void Enter()
    {
        Debug.Log("Estado do Terra");
        string animName = "KenzenTransformationEarth"; // Nome da animação a ser tocada

        SetAirStats();
        Debug.Log("batata" + !string.IsNullOrEmpty(animName));

        if (!string.IsNullOrEmpty(animName))
        {
            Debug.Log("Tocando a animação");
            unit.GetAnimator().Play(animName, 0);

            // Pega duração da animação com segurança
            AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
            animationLength = clip != null ? clip.length : 0.5f; // fallback se não encontrar

            // Ativa detecção de inimigos
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
        float tempSpeed = unit.Status.currentMoveSpeed * 0.1f;
        float tempDamage = unit.Status.currentAttackDamage * 2.5f;

        unit.Status.ModifyCurrentValue(AttributeType.currentMoveSpeed, -tempSpeed); // Diminui a velocidade
        unit.Status.ModifyCurrentValue(AttributeType.currentAttackDamage, tempDamage); // Aumenta o dano
    }
    public IEnumerator WaitForAnimation(float duration)
    {
        yield return new WaitForSeconds(duration);
        unit.PlayerSkillController.canAct = true;
        unit.ChangeState<IdleState>();
    }
}
