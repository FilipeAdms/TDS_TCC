using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MahhorMoveState : MahhorState
{
    public MahhorMoveState(MahhorStateMachine unit) : base(unit) { } // Construtor que recebe a unidade

    int patrolIndex;

    public override void Enter()
    {
        patrolIndex = Random.Range(0, 4);
    }

    public override void Tick()
    {
        
        if (unit.Transforms.position == unit.MahhorController.patrolPoints[patrolIndex].transform.position)
        {
            unit.ChangeState<MahhorIdleState>();
        } else
        {
            MoveTo(unit.MahhorController.patrolPoints[patrolIndex].transform.position);
        }
    }

    public override void Exit()
    {
        unit.Rigidbody2d.velocity = Vector2.zero;
        unit.StartCoroutine(CanActCooldown());
    }

    private void MoveTo(Vector2 targetPosition)
    {
        unit.Transforms.position = Vector2.MoveTowards(unit.Transforms.position, targetPosition, unit.Status.currentMoveSpeed * Time.deltaTime);
    }

    private IEnumerator CanActCooldown()
    {
        yield return new WaitForSeconds(1f);
        unit.MahhorSkillController.canAct = true;
    }
}
