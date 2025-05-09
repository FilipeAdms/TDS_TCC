using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorIdleState : MahhorState
{
    public MahhorIdleState(MahhorStateMachine unit) : base(unit) { }

    public override void Enter() {
        unit.GetAnimator().Play("Idle");
        unit.StartCoroutine(IdleTime());
    }
    public override void Tick() { }
    public override void Exit() { }

    private IEnumerator IdleTime()
    {
        yield return new WaitForSeconds(5f);
        unit.MahhorSkillController.ChooseSkill();
    }
}
