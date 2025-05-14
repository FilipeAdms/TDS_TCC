using System.Collections;
using UnityEngine;

public class MahhorAttackState : MahhorState
{
    public MahhorAttackState(MahhorStateMachine unit) : base(unit)
    {
    }
    private GameObject player;
    private Vector2 playerPosition;
    private Vector2 targetPosition;

    public override void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FindPlayer();
        unit.StartCoroutine(AttackTime());
    }
    public override void Tick()
    {
        MoveTo(targetPosition);
        
    }
    public override void Exit()
    {
        unit.MahhorSkillController.canAct = true;
    }

    private void FindPlayer()
    {
        playerPosition = player.transform.position;
        Vector2 direction = playerPosition - (Vector2)unit.Transforms.position;
        // Determina a direção dominante
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x >= 0)
            {
                targetPosition = playerPosition + Vector2.left;
            }
            else
            {
                targetPosition = playerPosition + Vector2.right;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                targetPosition = playerPosition + Vector2.down;
            }
            else
            {
                targetPosition = playerPosition + Vector2.up;
            }
        }
    }

    private void MoveTo(Vector2 targetPosition)
    {
        unit.Transforms.position = Vector2.MoveTowards(unit.Transforms.position, targetPosition, unit.Status.currentMoveSpeed * Time.deltaTime * 10f);
    }

    private IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(1f);
        unit.MahhorSkillController.canAct = true;
        unit.MahhorSkillController.ChooseSkill();
    }
}
