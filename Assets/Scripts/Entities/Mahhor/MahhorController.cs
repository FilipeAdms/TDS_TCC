using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MahhorController : MonoBehaviour
{
    private MahhorStateMachine unit;
    [SerializeField] private StatusComponent status;
    [SerializeField] private MahhorBars mahhorBars;
    [SerializeField] private MahhorSkillController skillController;
    [SerializeField] private MahhorDeath mahhorDeath;
    public MahhorTransformation currentTransformation;

    private int deathCount = 0; // Contador de mortes
    private bool isDead = false; // Flag para verificar se o Mahhor está morto

    [Header("Referências dos pontos de Patrulha")]
    public List<GameObject> patrolPoints = new List<GameObject>();

    private void Start()
    {

        isDead = false;
        deathCount = 0; // Inicializa o contador de mortes
        currentTransformation = MahhorTransformation.Default;
        status = GetComponent<StatusComponent>();
        unit = GetComponent<MahhorStateMachine>();

        status.ModifyCurrentValue(AttributeType.currentHealth, 500);
        status.ModifyBaseValue(AttributeType.maxHealth, 500);

        mahhorBars.SetMaxHealth(status.maxHealth);
        mahhorBars.SetHealth(status.currentHealth);

    }

    private void Update()
    {
        if (status.currentHealth < 1 && deathCount == 0 && !isDead)
        {
            deathCount++;
            status.ModifyBaseValue(AttributeType.maxHealth, 350);
            mahhorBars.SetMaxHealth(status.maxHealth);
            currentTransformation = MahhorTransformation.Transforming;
            skillController.canAct = false;
            unit.ChangeState<MahhorTransformationState>();
        }
        if (status.currentHealth < 1 && deathCount > 0 && !isDead)
        {
            isDead = true;
            mahhorDeath.DeathScene();
        }
        if (isDead)
        {
            skillController.canAct = false;
        }
    }

}
public enum MahhorTransformation
{
    Default,
    Transforming,
    Madness
}