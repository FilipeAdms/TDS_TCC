using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorIdleState : MahhorState
{
    public MahhorIdleState(MahhorStateMachine unit) : base(unit) { }

    private readonly float idleTimeDefault = 3.5f; // Tempo de espera para a transformação padrão
    private readonly float idleTimeMadness = 2f; // Tempo de espera para a transformação de loucura

    public override void Enter()
    {

        if (unit.MahhorController.currentTransformation == MahhorTransformation.Default)
        {
            unit.GetAnimator().Play("Idle");
            unit.StartCoroutine(IdleTime());
        }
        else if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            unit.GetAnimator().Play("MahhorIdleTransformed");
            unit.StartCoroutine(IdleTime());
        }
        else
        {
            Debug.LogError("Transformação desconhecida: " + unit.MahhorController.currentTransformation);
        }
    }
    public override void Tick() { }
    public override void Exit() { }

    private IEnumerator IdleTime()
    {
        if (unit.MahhorController.currentTransformation == MahhorTransformation.Default)
        {
            yield return new WaitForSeconds(idleTimeDefault);
            unit.MahhorSkillController.ChooseSkill();
        }
        else if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
        {
            yield return new WaitForSeconds(idleTimeMadness);
            unit.MahhorSkillController.ChooseSkill();
        }
    }
}
