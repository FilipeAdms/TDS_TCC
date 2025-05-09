using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorSkillController : MonoBehaviour
{
    [SerializeField] private MahhorStateMachine unit;
    [SerializeField] Stalagmit stalagmit;
    [SerializeField] StonePillar stonePillar;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ChooseSkill()
    {
        // Gera um número aleatório de 1 a 100 (quando random é com int, ele considera >= X e < y
        // quando é float ele considera >= X e <= Y
        int skillIndex = Random.Range(1, 101);

        if (skillIndex <= 33) 
        {
            unit.ChangeState<MahhorMoveState>();
        }
        else if(skillIndex >33 && skillIndex <= 66)
        {
            stalagmit.RangeDetection();
        }
        else if (skillIndex > 66)
        {
            stonePillar.RangeDetection();
        }
        //else if(skillIndex > 40)
        //{
            //unit.ChangeState<MahhorAttackState>();
        //}
    }

}
