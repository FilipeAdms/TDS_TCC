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
        originalColor = unit.SpriteRenderer.color;

        string hexCode = "#b8e6da"; // Valor padrão

        switch (unit.PlayerController.currentElement)
        {
            case ElementType.Default:
                hexCode = "#3b1e4d"; 
                break;
            case ElementType.Earth:
                hexCode = "#f1c40f";
                break;
            case ElementType.Air:
                hexCode = "#1565c0";
                break;
        }

        if (ColorUtility.TryParseHtmlString(hexCode, out Color hexBlinkColor))
        {
            blinkColor = hexBlinkColor;
        }


        dashDuration = 0.25f;

        if (unit.PlayerController.currentElement == ElementType.Default)
        {
            dashingSpeed = 2.25f;
        }
        else if (unit.PlayerController.currentElement == ElementType.Earth)
        {
            dashingSpeed = 1.75f;
        }
        else if (unit.PlayerController.currentElement == ElementType.Air)
        {
            dashingSpeed = 3f;
        }

        isDashing = true;
        unit.StartCoroutine(BlinkColorEffect());
        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(unit.transform.localScale.x, 0);
        }

        if (isDashing)
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
