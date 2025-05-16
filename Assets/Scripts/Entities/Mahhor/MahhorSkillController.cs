using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorSkillController : MonoBehaviour
{
    [SerializeField] private MahhorStateMachine unit;
    [SerializeField] Stalagmit stalagmit;
    [SerializeField] StonePillar stonePillar;
    [SerializeField] MahhorSkillController skillController;

    #region Flags
    public bool canAct = true;
    private bool isStalagmitActive = false;
    private bool isStonePillarActive = false;
    #endregion

    #region CoolDown's
    private float stalagmitCooldown = 5f;
    #endregion

    private void Update()
    {
        //Destinado a testes
    }

    public void ChooseSkill()
    {
        Debug.Log("Escolhendo habilidade");
        if (canAct)
        {
            canAct = false;
            // Gera um número aleatório de 1 a 100 (quando random é com int, ele considera >= X e < y
            // quando é float ele considera >= X e <= Y
            int skillIndex = Random.Range(1, 101);
            Debug.Log("Numero aleatorio de 1 a 100: "+ skillIndex);

            if (skillIndex <= 10)
            {
                Debug.Log("Escolhendo MahhorMoveState");
                unit.ChangeState<MahhorMoveState>();
            }
            else if (skillIndex > 10 && skillIndex <= 25 && !isStalagmitActive)
            {
                Debug.Log("Escolhendo stalagmit");
                isStalagmitActive = true;
                stalagmit.RangeDetection();
            }
            else if (skillIndex > 25 && skillIndex <= 45 && !isStonePillarActive)
            {
                Debug.Log("Escolhendo stonePillar");
                isStonePillarActive = true;
                stonePillar.RangeDetection();
            }
            else if(skillIndex > 45)
            {
                Debug.Log("Escolhendo MahhorAttackState");
                unit.ChangeState<MahhorAttackState>();
            }
            else
            {
                unit.ChangeState<MahhorMoveState>();
            }
        }
    }

    public IEnumerator StalagmitCooldown()
    {
        yield return new WaitForSeconds(stalagmitCooldown);
        Debug.Log("Stalagmit Cooldown finalizado");
        isStalagmitActive = false;
    }
    public IEnumerator StonePillarCooldown()
    {
        yield return new WaitForSeconds(stalagmitCooldown);
        Debug.Log("Stalagmit Cooldown finalizado");
        isStonePillarActive = false;
    }

}
