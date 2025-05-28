using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public DashState(PlayerStateMachine unit) : base(unit){ }

    private float dashingSpeed;
    private float dashDuration;
    private float blinkDuration = 0.05f;
    private Vector2 dashDirection;
    private bool isDashing;
    private Color blinkColor = Color.white;
    private Color originalColor;

    public override void Enter() 
    {
        originalColor = unit.SpriteRenderer.color; // Armazena a cor original do sprite
        dashDuration = 0.25f; // Duração do dash padrão

        if (unit.PlayerController.currentElement == ElementType.Default)
        {
            dashingSpeed = 2.5f; // Velocidade de dash padrão
        }
        else if (unit.PlayerController.currentElement == ElementType.Earth)
        {
            dashingSpeed = 1.8f; // Velocidade de dash padrão
        }
        else if (unit.PlayerController.currentElement == ElementType.Air)
        {
            dashingSpeed = 4f; // Velocidade de dash padrão
        }

        isDashing = true;
        unit.StartCoroutine(BlinkColorEffect()); // Inicia o efeito de piscar durante o dash
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

    IEnumerator BlinkColorEffect()
    {
        do
        {
            unit.SpriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            unit.SpriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        } while (isDashing);
    }
}
