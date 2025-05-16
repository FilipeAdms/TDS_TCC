using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorSetStatus : MonoBehaviour
{
    [SerializeField] private StatusComponent status;
    [SerializeField] private MahhorBars bars;

    #region EasyMultiplier
    private float currentHealthMultiplier;
    private float currentAuraMultiplier;
    private float currentMoveSpeedMultiplier;
    private float currentAttackDamageMultiplier;
    private float currentDefenseMultiplier;
    private float currentAuraRegenMultiplier;
    private float currentHealthRegenMultiplier;

    private float maxHealthMultiplier;
    private float maxAuraMultiplier;
    private float baseMoveSpeedMultiplier;
    private float baseAttackDamageMultiplier;
    private float baseDefenseMultiplier;
    private float baseHealthRegenMultiplier;
    private float baseAuraRegenMultiplier;
    #endregion

    #region HardMultiplier

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        setMultipliers("hard"); // apenas para teste inicial
    }

    public void SetStatus()
    {
        status.ModifyCurrentValue(AttributeType.currentHealth, (status.inicialHealth * currentHealthMultiplier) - status.currentHealth);
        status.ModifyCurrentValue(AttributeType.currentAura, (status.inicialAura * currentAuraMultiplier) - status.currentAura);
        status.ModifyCurrentValue(AttributeType.currentMoveSpeed, (status.inicialMoveSpeed * currentMoveSpeedMultiplier) - status.currentMoveSpeed);
        status.ModifyCurrentValue(AttributeType.currentAttackDamage, (status.inicialAttackDamage * currentAttackDamageMultiplier - status.currentAttackDamage));
        status.ModifyCurrentValue(AttributeType.currentDefense, (status.inicialDefense * currentDefenseMultiplier) - status.currentDefense);
        status.ModifyCurrentValue(AttributeType.currentAuraRegen, (status.inicialAuraRegen * currentAuraRegenMultiplier) - status.currentAuraRegen);
        status.ModifyCurrentValue(AttributeType.currentHealthRegen, (status.inicialHealthRegen * currentHealthRegenMultiplier) - status.currentHealthRegen);

        status.ModifyBaseValue(AttributeType.maxHealth, (status.inicialHealth * maxHealthMultiplier) - status.maxHealth);
        status.ModifyBaseValue(AttributeType.maxAura, (status.inicialAura * maxAuraMultiplier) - status.maxAura);
        status.ModifyBaseValue(AttributeType.baseMoveSpeed, (status.inicialMoveSpeed * baseMoveSpeedMultiplier) - status.baseMoveSpeed);
        status.ModifyBaseValue(AttributeType.baseAttackDamage, (status.inicialAttackDamage * baseAttackDamageMultiplier) - status.baseAttackDamage);
        status.ModifyBaseValue(AttributeType.baseDefense, (status.inicialDefense * baseDefenseMultiplier) - status.baseDefense);
        status.ModifyBaseValue(AttributeType.baseAuraRegen, (status.inicialAuraRegen * baseAuraRegenMultiplier) - status.baseAuraRegen);
        status.ModifyBaseValue(AttributeType.baseHealthRegen, (status.inicialHealthRegen * baseHealthRegenMultiplier) - status.baseHealthRegen);

        bars.SetMaxHealth(status.maxHealth);
        bars.SetHealth(status.currentHealth);
    }

    private void setMultipliers(string difficulty)
    {
        switch (difficulty)
        {
            case "easy":
                currentHealthMultiplier = 0.75f;
                currentAuraMultiplier = 0.75f;
                currentMoveSpeedMultiplier = 0.75f;
                currentAttackDamageMultiplier = 0.75f;
                currentDefenseMultiplier = 0.5f;
                currentAuraRegenMultiplier = 0.5f;
                currentHealthRegenMultiplier = 0.75f;

                maxHealthMultiplier = 0.75f;
                maxAuraMultiplier = 0.75f;
                baseMoveSpeedMultiplier = 0.75f;
                baseAttackDamageMultiplier = 0.75f;
                baseDefenseMultiplier = 0.5f;
                baseHealthRegenMultiplier = 0.5f;
                baseAuraRegenMultiplier = 0.75f;
                break;
            case "medium":
                currentHealthMultiplier = 1f;
                currentAuraMultiplier = 1f;
                currentMoveSpeedMultiplier = 1f;
                currentAttackDamageMultiplier = 1f;
                currentDefenseMultiplier = 1f;
                currentAuraRegenMultiplier = 1f;
                currentHealthRegenMultiplier = 1f;

                maxHealthMultiplier = 1f;
                maxAuraMultiplier = 1f;
                baseMoveSpeedMultiplier = 1f;
                baseAttackDamageMultiplier = 1f;
                baseDefenseMultiplier = 1f;
                baseHealthRegenMultiplier = 1f;
                baseAuraRegenMultiplier = 1f;
                break;
            case "hard":
                currentHealthMultiplier = 4f;
                currentAuraMultiplier = 4f;
                currentMoveSpeedMultiplier = 1.5f;
                currentAttackDamageMultiplier = 2.5f;
                currentDefenseMultiplier = 2f;
                currentAuraRegenMultiplier = 2f;
                currentHealthRegenMultiplier = 2f;

                maxHealthMultiplier = 4f;
                maxAuraMultiplier = 4f;
                baseMoveSpeedMultiplier = 1.5f;
                baseAttackDamageMultiplier = 2.5f;
                baseDefenseMultiplier = 2f;
                baseHealthRegenMultiplier = 2f;
                baseAuraRegenMultiplier = 2f;
                break;
        }
    }
}
