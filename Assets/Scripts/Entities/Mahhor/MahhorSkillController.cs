using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorSkillController : MonoBehaviour
{
    [SerializeField] private MahhorStateMachine unit;
    [SerializeField] Stalagmit stalagmit;
    [SerializeField] StonePillar stonePillar;

    #region Flags
    public bool canAct = true;
    public bool isStalagmitActive = false;
    public bool isStonePillarActive = false;
    #endregion

    #region CoolDown's
    private float stalagmitCooldown = 5f;
    private float stonePillarCooldown = 7f;
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
        yield return new WaitForSeconds(1f);
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
            unit.GetAnimator().Play("Idle");
            StartCoroutine(WaitForCanAct());
        }
    }
}
