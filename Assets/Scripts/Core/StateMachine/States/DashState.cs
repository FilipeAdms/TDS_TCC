using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public DashState(PlayerStateMachine unit) : base(unit){ }

    private float dashingSpeed;
    private float dashDuration;
    private Vector2 dashDirection;
    private bool isDashing;

    public override void Enter() 
    {
        if (unit.PlayerController.currentElement == ElementType.Default)
        {
            dashingSpeed = 3f; // Velocidade de dash padrão
            dashDuration = 0.25f; // Duração do dash padrão
        }
        else if (unit.PlayerController.currentElement == ElementType.Earth)
        {
            dashingSpeed = 2f; // Velocidade de dash padrão
            dashDuration = 0.3f; // Duração do dash padrão
        }
        else if (unit.PlayerController.currentElement == ElementType.Air)
        {
            dashingSpeed = 4.5f; // Velocidade de dash padrão
            dashDuration = 0.1f; // Duração do dash padrão
        }

        isDashing = true;
        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(unit.transform.localScale.x, 0); // Default direction
        }

        if(isDashing)
        {
            unit.Rigidbody2d.velocity = dashingSpeed * unit.Status.currentMoveSpeed * dashDirection;
        }
        unit.StartCoroutine(StopDashing());
    }
    public override void Tick() { }
    public override void Exit() { }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashDuration);
        unit.PlayerSkillController.canAct = true;
        isDashing = false;
        unit.Rigidbody2d.velocity = Vector2.zero;
        unit.ChangeState<IdleState>();
    }
}
