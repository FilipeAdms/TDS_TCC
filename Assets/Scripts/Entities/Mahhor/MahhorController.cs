using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MahhorController : MonoBehaviour
{
    private MahhorStateMachine unit;
    [SerializeField]private StatusComponent status;
    [SerializeField] private MahhorBars mahhorBars;
    [SerializeField] private MahhorSkillController skillController;

    [Header("Referências dos pontos de Patrulha")]
    public List<GameObject> patrolPoints = new List<GameObject>();

    private void Start()
    {
        status = GetComponent<StatusComponent>();
        unit = GetComponent<MahhorStateMachine>();

        status.ModifyCurrentValue(AttributeType.currentHealth, 300);
        status.ModifyBaseValue(AttributeType.maxHealth, 300);

        mahhorBars.SetMaxHealth(status.maxHealth);
        mahhorBars.SetHealth(status.currentHealth);

    }

    private void Update()
    {
        if (status.currentHealth < 1)
        {
            SceneManager.LoadScene("Menu");
        }
    }

}
