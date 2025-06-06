using System;
using System.Collections;
using UnityEngine;

public class MahhorTransformationState : MahhorState
{
    public MahhorTransformationState(MahhorStateMachine unit) : base(unit) { }

    private GameObject player;
    private Transform playerPosition;
    private string animName = "MahhorTransformation"; // Nome da anima��o a ser tocada
    private float animationLength; // Dura��o da anima��o
    private bool canPush = true;


    public override void Enter()
    {
        
        unit.MahhorSound.PlayTransformation();
        unit.Status.ModifyCurrentValue(AttributeType.currentHealth, 10);
        unit.StartCoroutine(HealthRegen());
        Debug.Log("Estado de Transforma��o de Mahhor");
        //Pegando o tempo de anima��o do ataque
        if (!string.IsNullOrEmpty(animName))
        {
            unit.GetAnimator().Play(animName, 0);

            // Pega dura��o da anima��o com seguran�a
            AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
            animationLength = clip != null ? clip.length : 1f; // Dura��o padr�o de 1 segundo se a anima��o n�o for encontrada
        }

        player = GameObject.FindGameObjectWithTag("Player"); // Procura pelo jogador
        playerPosition = player.transform; // Pega a posi��o do jogador

        unit.Status.ModifyCurrentValue(AttributeType.currentMoveSpeed, unit.Status.currentMoveSpeed * 4f);

        unit.StartCoroutine(WaitForAnimation(animationLength));
    }
    public override void Tick() {
        Debug.Log("Expulsando Jogador");

        if (Vector3.Distance(unit.Transforms.transform.position, playerPosition.position) < 5f && canPush)
        {
            Vector2 direction = (playerPosition.position - unit.Transforms.transform.position).normalized;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                float force = 2f; // Ajuste esse valor conforme a for�a desejada
                rb.AddForce(direction * force, ForceMode2D.Impulse);
                unit.MahhorSound.PlayTransformation();

            }
        }
    }
    public override void Exit() {
        unit.MahhorSound.PlayTransformation();
        Debug.Log("Saindo do estado de transforma��o de Mahhor");
    }


    private IEnumerator WaitForAnimation(float duration) // Espera o Tempo de anima��o acabar
    {
        Debug.Log("Esperando pela anima��o de transforma��o de Mahhor");
        yield return new WaitForSeconds(duration);
        Debug.Log("Anima��o de transforma��o de Mahhor conclu�da");
        unit.GetAnimator().Play("MahhorIdleTransformed"); // Toca a anima��o de Mahhor transformado
        yield return new WaitUntil(() => unit.Status.currentHealth >= unit.Status.maxHealth);
        unit.MahhorController.currentTransformation = MahhorTransformation.Madness;
        unit.MahhorSound.ChanceMusic();
        unit.MahhorSkillController.canAct = true; // Permite que Mahhor fa�a a��es
        unit.ChangeState<MahhorMoveState>();
    }

    private IEnumerator HealthRegen()
    {
        while (unit.Status.currentHealth < unit.Status.maxHealth)
        {
            unit.Status.ModifyCurrentValue(AttributeType.currentHealth, unit.Status.currentHealthRegen);
            unit.MahhorBars.SetHealth(unit.Status.currentHealth);
            yield return new WaitForSeconds(0.15f);
        }
        if (unit.Status.currentHealth > unit.Status.maxHealth)
        {
            canPush = false; // Desativa a expuls�o do jogador ap�s a transforma��o
            unit.Status.ModifyCurrentValue(AttributeType.currentHealth, unit.Status.maxHealth - unit.Status.currentHealth);
        }
        yield return null; // Espera um frame antes de continuar a execu��o
    }

}
