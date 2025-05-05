using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private UnitStateMachine unit;
    private StatusComponent status;
    public bool canAttack = true;
    public bool canDash = true;
    [SerializeField] private PlayerBars playerBars;

    private void Start()
    {
        status = GetComponent<StatusComponent>();
        unit = GetComponent<UnitStateMachine>();

        playerBars.SetMaxHealth(status.maxHealth); // Define a barra de vida inicial
        playerBars.SetHealth(status.currentHealth); // Atualiza a barra de vida inicial
        unit.trailRenderer.emitting = false; // Desativa o TrailRenderer no início
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && canAttack)
        {
            canAttack = false;
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.K) && canDash)
        {
            canDash = false;
            Dash();
        }
    }

    private void Attack()
    {
        Debug.Log("sla eu: "+ Equals(unit));
        unit.ChangeState<AttackState>();
    }

    private void Dash()
    {
        Debug.Log("sla eu: " + Equals(unit));
        unit.ChangeState<DashState>();
    }
}
