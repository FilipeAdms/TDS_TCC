using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    #region Variáveis
    private float hMov; // Armazena a velocidade horizontal
    private float vMov; // Armazena a velocidade vertical
    private Rigidbody2D rb; // Armazena o componente Rigidbody do jogador
    private Stats_Data stats_Data; // Armazena os dados do jogador
    private StateMachine stateMachine; // Máquina de estados

    // Estados do jogador
    private KenzenIdleState idleState;
    private KenzenRunningState runningState;
    private KenzenRunningTopState runningTopState;
    private KenzenRunningDownState runningDownState;
    private State state; // Estado atual do jogador
    #endregion
    void Start()
    {
        stats_Data = GetComponent<Stats_Data>();
        rb = GetComponent<Rigidbody2D>();

        // Criando a máquina de estados e os estados do jogador
        stateMachine = new StateMachine(); // Inicializa a máquina de estados
        Animator animator = GetComponent<Animator>();

        idleState = new KenzenIdleState(this, stateMachine, animator);
        runningState = new KenzenRunningState(this, stateMachine, animator);
        runningTopState = new KenzenRunningTopState(this, stateMachine, animator);
        runningDownState = new KenzenRunningDownState(this, stateMachine, animator);

        // Define o estado inicial
        stateMachine.ChangeState(idleState);
    }


    void Update()
    {
        Movement();

        if (stateMachine == null)
        {
            Debug.LogError("StateMachine está NULL!");
            return;
        }

        if (stateMachine.currentState == null)
        {
            Debug.LogError("CurrentState da StateMachine está NULL!");
            return;
        }

        SelectState();

        stateMachine.Update();
    }

    private void Movement()
    {
        hMov = Input.GetAxisRaw("Horizontal");
        vMov = Input.GetAxisRaw("Vertical");

        Vector2 movDirection = new Vector2(hMov, vMov).normalized;
        rb.velocity = movDirection * stats_Data.characterData.movSpeed;

        if (hMov > 0)
        {
        Debug.Log($"X {hMov}");
            rb.transform.eulerAngles = new Vector3(0f,0f,0f); // Vira para a direita
        }
        else if (hMov < 0)
        {
        Debug.Log($"X {hMov}");
            rb.transform.eulerAngles = new Vector3(0f, -180f, 0f); // Vira para a direita
        }
    }
    private void SelectState()
    {

        if (rb.velocity == Vector2.zero) // Se a velocidade for zero, entra no Idle
        {
            stateMachine.ChangeState(idleState);
        }
        else if (vMov > 0) // Se está se movendo para cima
        {
            stateMachine.ChangeState(runningTopState);
        }
        else if (vMov < 0) // Se está se movendo para baixo
        {
            stateMachine.ChangeState(runningDownState);
        }
        else if (hMov != 0) // Se está se movendo para os lados
        {
            stateMachine.ChangeState(runningState);
        }
    }
}
