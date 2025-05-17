using System;
using System.Collections;
using UnityEngine;

public class MahhorAttackState : MahhorState
{
    public MahhorAttackState(MahhorStateMachine unit) : base(unit)
    {
    }
    private GameObject player; // Refer�ncia ao jogador
    private Vector3 playerPosition; // Posi��o do jogador
    private Vector3 targetPosition; // Ponto alvo para onde Mahhor vai se mover
    private Vector3 attackPointReference; // Ponto de refer�ncia para o ataque
    private float animationLength; // Dura��o da anima��o
    private bool isWalking; // Flag para saber se ele est� andando
    private int activationAmount = 0; // Contador de ativa��es
    string animName =""; // Nome da anima��o a ser tocada

    public override void Enter()
    {
        activationAmount = 0; // Reseta o contador de ativa��es
        isWalking = true; // Ativa a flag de movimento, pois ele vai se mover para o jogador
        player = GameObject.FindGameObjectWithTag("Player"); // Procura pelo jogador
        FindPlayerToWalk(); // Encontra o jogador para saber onde ativar a dire��o da anima��o de ataque
    }
    public override void Tick()
    {

        // Verifica se Mahhor chegou na posi��o alvo e se n�o est� andando
        if (Vector3.Distance(unit.Transforms.position, targetPosition) < 0.05f && !isWalking && activationAmount == 0)
        {
            activationAmount++; // Incrementa o contador de ativa��es
            //Pegando o tempo de anima��o do ataque
            if (!string.IsNullOrEmpty(animName))
            {
                unit.GetAnimator().Play(animName, 0);

                // Pega dura��o da anima��o com seguran�a
                AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
                animationLength = clip != null ? clip.length : 1f; // Dura��o padr�o de 1 segundo se a anima��o n�o for encontrada

                // Ativa detec��o de inimigos
                unit.FindPlayerOnAttack.FindPlayer(unit.Transforms.position + attackPointReference); // M�todo que vai detectar o jogador e causar Dano
                unit.StartCoroutine(WaitForAnimation(animationLength)); // Espera o tempo de anima��o acabar
            }
        }
        // Verifica se Mahhor chegou na posi��o alvo e se est� andando
        else if (unit.Transforms.position == targetPosition && isWalking)
        {
            isWalking = false; // Desativa a flag de movimento
        }
        else
        {
            MoveTo(targetPosition); // Move Mahhor para a posi��o indicada
        }
    }
    public override void Exit()
    {
        unit.MahhorSkillController.canAct = true; // Permite que Mahhor fa�a a��es novamente 
    }

    //Encontra o jogador para saber onde ativar a dire��o da anima��o de ataque
    private void FindPlayerToWalk()
    {
        playerPosition = player.transform.position; // Posi��o do jogador
        Debug.Log("Jogador encontrado, posi��o: " + playerPosition);
        Vector2 direction = playerPosition - (Vector3)unit.Transforms.position; // Dire��o do ataque
        // Determina a dire��o dominante
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

    private void MoveTo(Vector2 targetPosition) // Move Mahhor para a posi��o indicada
    {
        unit.Transforms.position = Vector2.MoveTowards(unit.Transforms.position, targetPosition, unit.Status.currentMoveSpeed * Time.deltaTime);
    }
    private IEnumerator WaitForAnimation(float duration) // Espera o Tempo de anima��o acabar
    {
        yield return new WaitForSeconds(duration);
        unit.ChangeState<MahhorMoveState>();
    }
}
