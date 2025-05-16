using System;
using System.Collections;
using UnityEngine;

public class MahhorAttackState : MahhorState
{
    public MahhorAttackState(MahhorStateMachine unit) : base(unit)
    {
    }
    private GameObject player;
    private Vector2 playerPosition;
    private Vector2 targetPosition;
    private float animationLength;
    string animName ="";

    public override void Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Procura pelo jogador
        FindPlayer(); // Encontra o jogador para saber onde ativar a dire��o da anima��o de ataque
        unit.StartCoroutine(AttackTime()); // Tempo para permitir que Mahhor fa�a alguma a��o novamente

        //Pegando o tempo de anima��o do ataque
        if (!string.IsNullOrEmpty(animName))
        {
            unit.GetAnimator().Play(animName, 0);

            // Pega dura��o da anima��o com seguran�a
            AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
            animationLength = clip != null ? clip.length : 1f; // Dura��o padr�o de 1 segundo se a anima��o n�o for encontrada

            // Ativa detec��o de inimigos
            unit.FindPlayerOnAttack.FindPlayer(playerPosition); // M�todo que vai detectar o jogador e causar Dano
            unit.StartCoroutine(WaitForAnimation(animationLength)); // Espera o tempo de anima��o acabar
        }
    }
    public override void Tick()
    {
        MoveTo(targetPosition); // Move Mahhor para a posi��o indicada
    }
    public override void Exit()
    {
        unit.MahhorSkillController.canAct = true; // Permite que Mahhor fa�a a��es novamente 
    }

    //Encontra o jogador para saber onde ativar a dire��o da anima��o de ataque
    private void FindPlayer()
    {
        playerPosition = player.transform.position; // Posi��o do jogador
        Vector2 direction = playerPosition - (Vector2)unit.Transforms.position; // Dire��o do ataque
        // Determina a dire��o dominante
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x >= 0)
            {
                animName = "MahhorBasicAttackRight";
                targetPosition = playerPosition + Vector2.left;
            }
            else
            {
                animName = "MahhorBasicAttackLeft";
                targetPosition = playerPosition + Vector2.right;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                animName = "MahhorBasicAttackUp";
                targetPosition = playerPosition + Vector2.down;
            }
            else
            {
                animName = "MahhorBasicAttackDown";
                targetPosition = playerPosition + Vector2.up;
            }
        }
    }

    private void MoveTo(Vector2 targetPosition) // Move Mahhor para a posi��o indicada
    {
        unit.Transforms.position = Vector2.MoveTowards(unit.Transforms.position, targetPosition, unit.Status.currentMoveSpeed * Time.deltaTime);
    }

    private IEnumerator AttackTime() // Tempo para permitir que Mahhor fa�a alguma a��o novamente
    {
        yield return new WaitForSeconds(2f);
        unit.MahhorSkillController.canAct = true;
    }
    private IEnumerator WaitForAnimation(float duration) // Espera o Tempo de anima��o acabar
    {
        yield return new WaitForSeconds(duration);
        unit.MahhorSkillController.ChooseSkill();
    }
}
