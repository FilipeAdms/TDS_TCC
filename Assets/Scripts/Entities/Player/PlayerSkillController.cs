using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillController : MonoBehaviour
{
    public bool canAct = true; // Indica se o jogador pode realizar ações
    public bool canDash = true;
    public bool canTransform = true;
    private PlayerStateMachine unit;
    private TransformationState transformationState = TransformationState.Ready;
    private float transformationDuration = 15f;
    private float chargingTimer;
    private float transformationTimer;
    [SerializeField] private Slider transformationSlider; // Slider para a transformação
    [SerializeField] private TemplateChanging templateChanging; // Referência ao script de mudança de template


    private float cooldownDash = 0.3f;

    private void Start()
    {
        transformationSlider.maxValue = transformationDuration; // Define o valor máximo do slider
        transformationSlider.value = transformationDuration; // Define o valor máximo do slider
        unit = GetComponent<PlayerStateMachine>();
    }
    private void Update()
    {
        DashInput();
        TransformationInput();
        TransformationTimer();
        RechargeTimer();
    }



    private IEnumerator StartCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canDash = true;
    }
    private void Dash()
    {
        StartCoroutine(StartCooldown(cooldownDash));
        unit.ChangeState<DashState>();
    }
    private void EarthTransformation()
    {
        unit.PlayerController.currentElement = ElementType.Earth;
        //Ainda não existe
    }

    private void DashInput()
    {
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.X)) &&
            (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) &&
            canDash && canAct)
        {
            canAct = false;
            canDash = false;
            Dash();
        }
    }
    private void TransformationInput() // Método para lidar com a entrada de transformação do jogador
    {
        if (transformationState == TransformationState.Ready &&
            Input.GetKey(KeyCode.LeftShift) &&
            Input.GetKeyDown(KeyCode.Z) && canAct && canTransform)
        {
            canAct = false;
            canTransform = false;
            transformationState = TransformationState.Transforming;
            transformationTimer = transformationDuration;

            unit.PlayerController.currentElement = ElementType.Air;
            templateChanging.AirTransformationTemplate();
            unit.ChangeState<AirTransformationState>();
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.X) && canTransform && canAct)
        {
            canAct = false;
            canTransform = false;
            templateChanging.EarthTransformationTemplate();
            EarthTransformation();
        }
    }
    private void TransformationTimer()
    {
        if (transformationState == TransformationState.Transforming && transformationTimer > 0)
        {
            transformationTimer -= Time.deltaTime;
            transformationSlider.value = transformationTimer;

            if (transformationTimer <= 0)
            {
                templateChanging.DefaultTransformationTemplate();
                unit.PlayerController.currentElement = ElementType.Default;
                chargingTimer = 0;
                transformationState = TransformationState.Cooldown;
            }
        }
    }
    private void RechargeTimer()
    {
        if (transformationState == TransformationState.Cooldown)
        {
            chargingTimer += Time.deltaTime * 3;
            transformationSlider.value = chargingTimer;

            if (chargingTimer >= transformationDuration)
            {
                canTransform = true;
                transformationState = TransformationState.Ready;
                transformationSlider.value = transformationDuration;
                Debug.Log("Já pode se transformar novamente");
            }
        }
    }

}

public enum TransformationState
{
    Ready,
    Transforming,
    Cooldown
}