using System;
using System.Collections;
using UnityEngine;

public class MahhorTransformationState : MahhorState
{
    public MahhorTransformationState(MahhorStateMachine unit) : base(unit) { }

    private GameObject player;
    private Transform playerPosition;
    private string animName = "MahhorTransformation"; // Nome da animação a ser tocada
    private float animationLength; // Duração da animação
    private bool canPush = true;


    public override void Enter()
    {
        
        unit.MahhorSound.PlayTransformation();
        unit.Status.ModifyCurrentValue(AttributeType.currentHealth, 10);
        unit.StartCoroutine(HealthRegen());
        Debug.Log("Estado de Transformação de Mahhor");
        //Pegando o tempo de animação do ataque
        if (!string.IsNullOrEmpty(animName))
        {
            unit.GetAnimator().Play(animName, 0);

            // Pega duração da animação com segurança
            AnimationClip clip = Array.Find(unit.GetAnimator().runtimeAnimatorController.animationClips, c => c.name == animName);
            animationLength = clip != null ? clip.length : 1f; // Duração padrão de 1 segundo se a animação não for encontrada
        }

        player = GameObject.FindGameObjectWithTag("Player"); // Procura pelo jogador
        playerPosition = player.transform; // Pega a posição do jogador

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
                float force = 2f; // Ajuste esse valor conforme a força desejada
                rb.AddForce(direction * force, ForceMode2D.Impulse);
                unit.MahhorSound.PlayTransformation();

            }
        }
    }
    public override void Exit() {
        unit.MahhorSound.PlayTransformation();
        Debug.Log("Saindo do estado de transformação de Mahhor");
    }


    private IEnumerator WaitForAnimation(float duration) // Espera o Tempo de animação acabar
    {
        Debug.Log("Esperando pela animação de transformação de Mahhor");
        yield return new WaitForSeconds(duration);
        Debug.Log("Animação de transformação de Mahhor concluída");
        unit.GetAnimator().Play("MahhorIdleTransformed"); // Toca a animação de Mahhor transformado
        yield return new WaitUntil(() => unit.Status.currentHealth >= unit.Status.maxHealth);
        unit.MahhorController.currentTransformation = MahhorTransformation.Madness;
        unit.MahhorSound.ChanceMusic();
        unit.MahhorSkillController.canAct = true; // Permite que Mahhor faça ações
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
            canPush = false; // Desativa a expulsão do jogador após a transformação
            unit.Status.ModifyCurrentValue(AttributeType.currentHealth, unit.Status.maxHealth - unit.Status.currentHealth);
        }
        yield return null; // Espera um frame antes de continuar a execução
    }

}
