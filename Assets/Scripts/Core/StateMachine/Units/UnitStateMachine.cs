using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour
{
    public State currentState { get; private set; } // A máquina de estados da unidade
    public StatusComponent status { get; private set; } // O componente de status da unidade

    private Animator animator; // O Animator da unidade
    //Um dictionery serve para armazenar pares de chave-valor, onde a chave é do tipo Type e o valor é do tipo State
    private Dictionary<Type, State> states; // Dicionário para armazenar os estados

    private void Awake()
    {
        status = GetComponent<StatusComponent>();

        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        // Inicializa o dicionário de estados, ou seja cria os estados
        states = new Dictionary<Type, State>
        {
            { typeof(PlayerIdleState), new PlayerIdleState(this) },
            { typeof(PlayerMoveState), new PlayerMoveState(this) },
            { typeof(DeathState), new DeathState(this) }
        };

        // Define o estado inicial como PlayerIdleState
        SetState(states[typeof(PlayerIdleState)]);
    }

    private void Update()
    {
        // Chama o Tick de cada estado
        currentState?.Tick();
    }

    // Método para trocar o estado
    public void SetState(State newState)
    {
        if (newState == null)
        {
            Debug.LogError("Novo estado não pode ser nulo.");
            return;
        }
        currentState?.Exit(); // Executa o Exit do estado atual
        currentState = newState; // Define o novo estado
        currentState.Enter(); // Executa o Enter do novo estado
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
        {
            SetState(newState); // Troca para o novo estado
        }
        else
        {
            Debug.LogError($"Estado {typeof(StateName).Name} não está registrado na máquina de estados.");
        }
    }
}
