using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PlayerStateMachine unit;
    private StatusComponent status;
    public bool canAttack = true;
    [SerializeField] private PlayerBars playerBars;

    private void Start()
    {
        status = GetComponent<StatusComponent>();
        unit = GetComponent<PlayerStateMachine>();

        playerBars.SetMaxHealth(status.maxHealth); // Define a barra de vida inicial
        playerBars.SetHealth(status.currentHealth); // Atualiza a barra de vida inicial
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && canAttack)
        {
            canAttack = false;
            Attack();
        }
        if (status.currentHealth < 1)
        {
            SceneManager.LoadScene("Menu");

        }
    }

    private void Attack()
    {
        unit.ChangeState<AttackState>();
    }

}
