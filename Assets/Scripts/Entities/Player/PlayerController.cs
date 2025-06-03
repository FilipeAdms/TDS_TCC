using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PlayerStateMachine unit;
    private StatusComponent status;
    public bool canAttack = true;
    public ElementType currentElement; // Elemento atual do jogador
    [SerializeField] private PlayerBars playerBars;

    private void Start()
    {
        currentElement = ElementType.Default; // Define o elemento inicial do jogador
        status = GetComponent<StatusComponent>();
        unit = GetComponent<PlayerStateMachine>();

        playerBars.SetMaxHealth(status.maxHealth); // Define a barra de vida inicial
        playerBars.SetHealth(status.currentHealth); // Atualiza a barra de vida inicial
    }
    // Update is called once per frame
    void Update()
    {
        // Ataque
        if((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z)) &&
            (!Input.GetKey(KeyCode.LeftShift))
            && canAttack)
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

public enum ElementType
{
    Earth,
    Air,
    Default
}
