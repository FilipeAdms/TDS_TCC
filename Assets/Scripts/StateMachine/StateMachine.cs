public class StateMachine
{
    public State currentState;

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit(); // Sai do estado atual
        }
        currentState = newState; // guarda o novo estado
        currentState.Enter(); // Entra no novo estado
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update(); // Atualiza o estado atual
        }
    }
}
