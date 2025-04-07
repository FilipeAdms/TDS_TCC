using UnityEngine;

public class State
{
    protected Animator animator;
    protected StateMachine stateMachine;
    protected MonoBehaviour owner; // O dono da animação, para ser específico

    public bool IsComplete { get; protected set; }
    protected float startTime;

    public float time => Time.time - startTime; // Tempo desde que o estado começou

    public State(MonoBehaviour owner, StateMachine stateMachine, Animator animator)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        this.animator = animator;
    }

    public virtual void Enter() { }  // Chamado ao entrar no estado
    public virtual void Update() { } // Chamado para atualizar o estado
    public virtual void Exit() { }   // Chamado ao sair do estado

    public virtual float GetAnimationLength(string animationName) { return 0; }// Retorna o tempo de uma animação
}
