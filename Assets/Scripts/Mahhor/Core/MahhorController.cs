using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorController : MonoBehaviour
{

    [SerializeField] private MahhorMovement mahhorMovement;
    [SerializeField] private MahhorSkills mahhorSkills;
    [SerializeField] private MahhorAttack mahhorAttack;

    public bool canAct = true;
    private float actionChance = 0;

    // Start is called before the first frame update
    void Start()
    {
        mahhorAttack = GetComponent<MahhorAttack>();
        mahhorMovement = GetComponent<MahhorMovement>();
        mahhorSkills = GetComponent<MahhorSkills>();
        ChooseAction();
    }
    private void FixedUpdate()
    {
        ChooseAction();
    }
    private void ChooseAction()
    {
        if (canAct)
        {
            canAct = false; // Desabilita a ação

            actionChance = Random.Range(1f, 100f);
            if (actionChance <= 45) // 45% de chance de atacar
            {
                Debug.Log("Hora de Atacar!");
                canAct = true; //TEMPORARIO
                ChooseAction();
            }
            else if (actionChance > 45 && actionChance <= 80) // 35% de chance de usar habilidade
            {
                mahhorSkills.ChooseSkill();
            }
            else if (actionChance > 80 && actionChance <= 100) // 20% de chance de se mover
            {
                Debug.Log("Hora de caminhar!");
                canAct = true; //TEMPORARIO
                ChooseAction();
            }
        }
    }
}
