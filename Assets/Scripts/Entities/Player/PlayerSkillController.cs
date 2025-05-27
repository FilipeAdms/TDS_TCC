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
    private float transformationTimer = 15f; // Temporizador para a transformação
    private float chargingTimer = 0f; // Temporizador para o carregamento da transformação
    [SerializeField] private Slider transformationSlider; // Slider para a transformação


    private float cooldownDash = 0.3f;

    private void Start()
    {
        transformationSlider.maxValue = transformationTimer; // Define o valor máximo do slider
        transformationSlider.value = transformationTimer; // Define o valor máximo do slider
        unit = GetComponent<PlayerStateMachine>();
    }
    private void Update()
    {
        //Dash
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.X)) &&
            (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) &&
            canDash && canAct)
        {
            canAct = false;
            canDash = false;
            Dash();
        }
        //Transformação
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z) && canTransform && canAct)
        {
            Debug.Log("Ar");
            canAct = false;
            canTransform = false;
            transformationTimer = 15f;
            chargingTimer = 0f;

            if (unit.PlayerController.currentElement != ElementType.Air)
            {
                Debug.Log("AirTransformation()");
                Debug.Log("Ativando o novo estado de transformação do Ar");
                unit.PlayerController.currentElement = ElementType.Air;
                unit.ChangeState<AirTransformationState>();
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.X) && canTransform && canAct)
        {
            canAct = false;
            canTransform = false;
            //EarthTransformation(); (ainda não existe)
        }
        // Reseta a transformação após o tempo definido
        if (!canTransform)
        {
            transformationTimer -= Time.deltaTime;
            transformationSlider.value = transformationTimer; // Atualiza o slider com o tempo restante
            if (transformationTimer <= 0.01)
            {
                unit.PlayerController.currentElement = ElementType.Default; // Muda para o estado de transformação padrão
            }
        }
        if(transformationTimer <= 0.01)
        {
            chargingTimer += Time.deltaTime * 3; // Incrementa o temporizador de carregamento
            transformationSlider.value = chargingTimer; // Atualiza o slider com o tempo restante
            if (transformationTimer >= 15f)
            {
                canTransform = true;
            }
        }
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
}