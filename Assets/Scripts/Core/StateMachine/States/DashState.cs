using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public DashState(PlayerStateMachine unit) : base(unit){ }

    private float dashingSpeed = 3f;
    private float dashDuration = 0.25f;
    private Vector2 dashDirection;
    private bool isDashing;

    public override void Enter() 
    {
        isDashing = true;
        unit.trailRenderer.emitting = true;
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
        isDashing = false;
        unit.Rigidbody2d.velocity = Vector2.zero;
        unit.trailRenderer.emitting = false;
        unit.ChangeState<IdleState>();
    }
}
