using System;
using System.Collections;
using UnityEngine;

public class MahhorAttackState : MahhorState
{
    public MahhorAttackState(MahhorStateMachine unit) : base(unit)
    {
    }
    private GameObject player; // Referência ao jogador
    private Vector3 playerPosition; // Posição do jogador
    private Vector3 targetPosition; // Ponto alvo para onde Mahhor vai se mover
    private Vector3 attackPointReference; // Ponto de referência para o ataque
    private float animationLength; // Duração da animação
    private bool isWalking; // Flag para saber se ele está andando
    private int activationAmount = 0; // Contador de ativações
    string animName =""; // Nome da animação a ser tocada

    public override void Enter()
    {
        activationAmount = 0; // Reseta o contador de ativações
        isWalking = true; // Ativa a flag de movimento, pois ele vai se mover para o jogador
        player = GameObject.FindGameObjectWithTag("Player"); // Procura pelo jogador
        FindPlayerToWalk(); // Encontra o jogador para saber onde ativar a direção da animação de ataque
    }
    public override void Tick()
    {

        // Verifica se Mahhor chegou na posição alvo e se não está andando
        if (Vector3.Distance(unit.Transforms.position, targetPosition) < 0.05f && !isWalking && activationAmount == 0)
        {
            activationAmount++; // Incrementa o contador de ativações
            //Pegando o tempo de animação do ataque
            if (!string.IsNullOrEmpty(animName))
            {
                unit.GetAnimator().Play(animName, 0);

                // Pega duração da animação com segurança
                AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
                animationLength = clip != null ? clip.length : 1f; // Duração padrão de 1 segundo se a animação não for encontrada

                // Ativa detecção de inimigos
                unit.FindPlayerOnAttack.FindPlayer(unit.Transforms.position + attackPointReference); // Método que vai detectar o jogador e causar Dano
                unit.StartCoroutine(WaitForAnimation(animationLength)); // Espera o tempo de animação acabar
            }
        }
        // Verifica se Mahhor chegou na posição alvo e se está andando
        else if (unit.Transforms.position == targetPosition && isWalking)
        {
            isWalking = false; // Desativa a flag de movimento
        }
        else
        {
            MoveTo(targetPosition); // Move Mahhor para a posição indicada
        }
    }
    public override void Exit()
    {
        unit.MahhorSkillController.canAct = true; // Permite que Mahhor faça ações novamente 
    }

    //Encontra o jogador para saber onde ativar a direção da animação de ataque
    private void FindPlayerToWalk()
    {
        playerPosition = player.transform.position; // Posição do jogador
        Debug.Log("Jogador encontrado, posição: " + playerPosition);
        Vector2 direction = playerPosition - (Vector3)unit.Transforms.position; // Direção do ataque
        // Determina a direção dominante
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x >= 0)
            {
                animName = "MahhorBasicAttackRight";
                targetPosition = playerPosition + Vector3.left;
                attackPointReference = Vector3.right;
            }
            else
            {
                animName = "MahhorBasicAttackLeft";
                targetPosition = playerPosition + Vector3.right;
                attackPointReference = Vector3.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                animName = "MahhorBasicAttackUp";
                targetPosition = playerPosition + Vector3.down;
                attackPointReference = Vector3.up;
            }
            else
            {
                animName = "MahhorBasicAttackDown";
                targetPosition = playerPosition + Vector3.up;
                attackPointReference = Vector3.down;
            }
        }
    }

    private void MoveTo(Vector2 targetPosition) // Move Mahhor para a posição indicada
    {
        unit.Transforms.position = Vector2.MoveTowards(unit.Transforms.position, targetPosition, unit.Status.currentMoveSpeed * Time.deltaTime);
    }
    private IEnumerator WaitForAnimation(float duration) // Espera o Tempo de animação acabar
    {
        yield return new WaitForSeconds(duration);
        unit.ChangeState<MahhorMoveState>();
    }
}
