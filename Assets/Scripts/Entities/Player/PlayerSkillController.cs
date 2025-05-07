using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    public bool canDash = true;
    private PlayerStateMachine unit;

    private float cooldownDash = 1.25f;

    private void Start()
    {
        unit = GetComponent<PlayerStateMachine>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) &&
            (Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0) &&
            canDash)
        {
            canDash = false;
            Dash();
        }
    }

    private IEnumerator StartCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canDash = true;
    }
    private void Dash()
    {
        StartCoroutine(StartCooldown(cooldownDash));
        unit.ChangeState<DashState>();
    }
}