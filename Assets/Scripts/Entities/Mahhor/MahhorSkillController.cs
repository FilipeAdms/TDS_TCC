using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorSkillController : MonoBehaviour
{
    [SerializeField] private MahhorStateMachine unit;
    [SerializeField] Stalagmit stalagmit;
    [SerializeField] StonePillar stonePillar;
    [SerializeField] MahhorController controller;

    #region Flags
    public bool canAct = true;
    public bool isStalagmitActive = false;
    public bool isStonePillarActive = false;
    #endregion

    #region CoolDown's
    private float stalagmitCooldown = 5f;
    private float stonePillarCooldown = 7f;
    private readonly float idleTimeDefault = 3.5f; // Tempo de espera para a transformação padrão
    private readonly float idleTimeMadness = 2f; // Tempo de espera para a transformação de loucura
    #endregion

    private void Update()
    {
        //Destinado a testes
    }

    public void ChooseSkill()
    {
        StartCoroutine(ChoosingSkill());
    }

    public IEnumerator StalagmitCooldown()
    {
        yield return new WaitForSeconds(stalagmitCooldown);
        isStalagmitActive = false;
    }
    public IEnumerator StonePillarCooldown()
    {
        yield return new WaitForSeconds(stonePillarCooldown);
        isStonePillarActive = false;
    }

    private IEnumerator WaitForCanAct()
    {
        yield return new WaitUntil(() => canAct == true);
        ChooseSkill();
    }

    private IEnumerator ChoosingSkill()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Escolhendo habilidade");
        if (canAct)
        {
            canAct = false;
            // Gera um número aleatório de 1 a 100 (quando random é com int, ele considera >= X e < y
            // quando é float ele considera >= X e <= Y
            int skillIndex = Random.Range(1, 101);

            if (skillIndex <= 10)
            {
                Debug.Log("MahhorMoveState");
                unit.ChangeState<MahhorMoveState>();
            }
            else if (skillIndex > 10 && skillIndex <= 25 && !isStalagmitActive)
            {
                Debug.Log("stalagmit");
                isStalagmitActive = true;
                stalagmit.RangeDetection();
            }
            else if (skillIndex > 25 && skillIndex <= 45 && !isStonePillarActive)
            {
                Debug.Log("stonePillar");
                isStonePillarActive = true;
                stonePillar.RangeDetection();
            }
            else if (skillIndex > 45)
            {
                Debug.Log("MahhorAttackState");
                unit.ChangeState<MahhorAttackState>();
            }
            else
            {
                unit.ChangeState<MahhorMoveState>();
            }
        }
        else
        {
            Debug.Log("Ação não pode ser realizada, esperando cooldown");
            if (unit.MahhorController.currentTransformation == MahhorTransformation.Default)
            {
                yield return new WaitForSeconds(idleTimeDefault);
                unit.GetAnimator().Play("Idle");
                StartCoroutine(WaitForCanAct());
            }
            else if (unit.MahhorController.currentTransformation == MahhorTransformation.Transforming)
            {
                yield return null;
            }
            else if (unit.MahhorController.currentTransformation == MahhorTransformation.Madness)
            {
                yield return new WaitForSeconds(idleTimeMadness);
                unit.GetAnimator().Play("MahhorIdleTransformed");
                unit.MahhorSkillController.ChooseSkill();
            }
            else
            {
                Debug.LogError("Transformação desconhecida: " + unit.MahhorController.currentTransformation);
            }

        }
    }
}


