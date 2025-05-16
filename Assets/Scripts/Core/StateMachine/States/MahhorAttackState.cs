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
        FindPlayer(); // Encontra o jogador para saber onde ativar a direção da animação de ataque
        unit.StartCoroutine(AttackTime()); // Tempo para permitir que Mahhor faça alguma ação novamente

        //Pegando o tempo de animação do ataque
        if (!string.IsNullOrEmpty(animName))
        {
            unit.GetAnimator().Play(animName, 0);

            // Pega duração da animação com segurança
            AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
            animationLength = clip != null ? clip.length : 1f; // Duração padrão de 1 segundo se a animação não for encontrada

            // Ativa detecção de inimigos
            unit.FindPlayerOnAttack.FindPlayer(playerPosition); // Método que vai detectar o jogador e causar Dano
            unit.StartCoroutine(WaitForAnimation(animationLength)); // Espera o tempo de animação acabar
        }
    }
    public override void Tick()
    {
        MoveTo(targetPosition); // Move Mahhor para a posição indicada
    }
    public override void Exit()
    {
        unit.MahhorSkillController.canAct = true; // Permite que Mahhor faça ações novamente 
    }

    //Encontra o jogador para saber onde ativar a direção da animação de ataque
    private void FindPlayer()
    {
        playerPosition = player.transform.position; // Posição do jogador
        Vector2 direction = playerPosition - (Vector2)unit.Transforms.position; // Direção do ataque
        // Determina a direção dominante
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

    private void MoveTo(Vector2 targetPosition) // Move Mahhor para a posição indicada
    {
        unit.Transforms.position = Vector2.MoveTowards(unit.Transforms.position, targetPosition, unit.Status.currentMoveSpeed * Time.deltaTime);
    }

    private IEnumerator AttackTime() // Tempo para permitir que Mahhor faça alguma ação novamente
    {
        yield return new WaitForSeconds(2f);
        unit.MahhorSkillController.canAct = true;
    }
    private IEnumerator WaitForAnimation(float duration) // Espera o Tempo de animação acabar
    {
        yield return new WaitForSeconds(duration);
        unit.MahhorSkillController.ChooseSkill();
    }
}
