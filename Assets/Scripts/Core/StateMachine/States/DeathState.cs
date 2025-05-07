using UnityEngine;

public class DeathState : State
{
    public DeathState(PlayerStateMachine unit) : base(unit) { }

    public override void Enter()
    {
        Debug.Log($"{unit.gameObject.name} morreu!");
        GameObject.Destroy(unit.gameObject);
    }
}