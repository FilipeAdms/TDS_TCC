using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MahhorStateMachine : MonoBehaviour
{
    public MahhorState CurrentState { get; private set; } // A m�quina de estados da unidade
    public StatusComponent Status { get; private set; } // O componente de status da unidade
    public Rigidbody2D Rigidbody2d { get; private set; } // O Rigidbody2D da unidade
    public Transform Transforms { get; private set; } // O Transform da unidade
    public FindEnemyOnAttack FindEnemyOnAttack { get; private set; } // O script de detec��o de inimigos
    public MahhorController MahhorController { get; private set; } // O controlador do Mahhor

    public MahhorSkillController MahhorSkillController { get; private set; } // O controlador da skill do Mahhor

    private Animator animator; // O Animator da unidade
    //Um dictionery serve para armazenar pares de chave-valor, onde a chave � do tipo Type e o valor � do tipo State
    private Dictionary<Type, MahhorState> states; // Dicion�rio para armazenar os estados

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Transforms = GetComponent<Transform>();

        Status = GetComponent<StatusComponent>();

        Rigidbody2d = GetComponent<Rigidbody2D>();

        MahhorController = GetComponent<MahhorController>();

        FindEnemyOnAttack = GetComponent<FindEnemyOnAttack>();

        MahhorSkillController = GetComponent<MahhorSkillController>();
    }

    private void Start()
    {
        // Inicializa o dicion�rio de estados, ou seja cria os estados
        states = new Dictionary<Type, MahhorState>
        {
            { typeof(MahhorMoveState), new MahhorMoveState(this) },
            { typeof(MahhorAttackState), new MahhorAttackState(this) },
            { typeof(MahhorIdleState), new MahhorIdleState(this) },
        };

        // Define o estado inicial como IdleState
        SetState(states[typeof(MahhorIdleState)]);
    }

    private void Update()
    {
        // Chama o Tick de cada estado
        CurrentState?.Tick();
    }

    // M�todo para trocar o estado
    public void SetState(MahhorState newState)
    {
        if (newState == null)
        {
            Debug.LogError("Novo estado n�o pode ser nulo.");
            return;
        }
        CurrentState?.Exit(); // Executa o Exit do estado atual
        CurrentState = newState; // Define o novo estado
        CurrentState.Enter(); // Executa o Enter do novo estado
    }

    // M�todo para acessar o Animator
    public Animator GetAnimator()
    {
        return animator;
    }

    // M�todo para trocar o estado dinamicamente usando o tipo
    public void ChangeState<StateName>() where StateName : MahhorState
    {
        // Verifica se o estado existe no dicion�rio
        if (states.TryGetValue(typeof(StateName), out var newState))
        //type of retorna o tipo do objeto, ou seja, o tipo do estado
        // ou var retorna o estado do dicion�rio
        {
            SetState(newState); // Troca para o novo estado
        }
        else
        {
            Debug.LogError($"Estado {typeof(StateName).Name} n�o est� registrado na m�quina de estados.");
        }
    }
}
