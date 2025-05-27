using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; } // A máquina de estados da unidade
    public StatusComponent Status { get; private set; } // O componente de status da unidade
    public Rigidbody2D Rigidbody2d { get; private set; } // O Rigidbody2D da unidade
    public Transform Transforms { get; private set; } // O Transform da unidade
    public FindEnemyOnAttack FindEnemyOnAttack { get; private set; } // O script de detecção de inimigos
    public PlayerController PlayerController { get; private set; } // O controlador do jogador
    public PlayerSkillController PlayerSkillController { get; private set; } // O controlador de habilidades do jogador

    private Animator animator; // O Animator da unidade
    //Um dictionery serve para armazenar pares de chave-valor, onde a chave é do tipo Type e o valor é do tipo State
    private Dictionary<Type, State> states; // Dicionário para armazenar os estados

    private void Awake()
    {
        Status = GetComponent<StatusComponent>();

        animator = GetComponent<Animator>();

        Rigidbody2d = GetComponent<Rigidbody2D>();

        Transforms = GetComponent<Transform>();

        PlayerController = GetComponent<PlayerController>();

        PlayerSkillController = GetComponent<PlayerSkillController>();

        FindEnemyOnAttack = GetComponent<FindEnemyOnAttack>();


    }

    private void Start()
    {
        // Inicializa o dicionário de estados, ou seja cria os estados
        states = new Dictionary<Type, State>
        {
            { typeof(IdleState), new IdleState(this) },
            { typeof(MoveState), new MoveState(this) },
            { typeof(AttackState), new AttackState(this) },
            { typeof(DashState), new DashState(this) },
            { typeof(DeathState), new DeathState(this) },
            { typeof(AirTransformationState), new AirTransformationState(this) }
        };

        // Define o estado inicial como IdleState
        SetState(states[typeof(IdleState)]);
    }

    private void Update()
    {
        // Chama o Tick de cada estado
        CurrentState?.Tick();
    }

    // Método para trocar o estado
    public void SetState(State newState)
    {
        if (newState == null)
        {
            return;
        }
        CurrentState?.Exit(); // Executa o Exit do estado atual
        CurrentState = newState; // Define o novo estado
        CurrentState.Enter(); // Executa o Enter do novo estado
    }

    // Método para acessar o Animator
    public Animator GetAnimator()
    {
        return animator;
    }

    // Método para trocar o estado dinamicamente usando o tipo
    public void ChangeState<StateName>() where StateName : State
    {
        // Verifica se o estado existe no dicionário
        if (states.TryGetValue(typeof(StateName), out var newState))
        //type of retorna o tipo do objeto, ou seja, o tipo do estado
        // ou var retorna o estado do dicionário
        {
            SetState(newState); // Troca para o novo estado
        }
    }
}
