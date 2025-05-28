using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillController : MonoBehaviour
{
    public bool canAct = true; // Indica se o jogador pode realizar a��es
    public bool canDash = true;
    public bool canTransform = true;
    private PlayerStateMachine unit;
    private TransformationState transformationState = TransformationState.Ready;
    private float transformationDuration = 15f;
    private float timer = 0f;
    private float transformationCooldown = 5f;
    private float chargingTimer;
    private float transformationTimer;
    [SerializeField] private Slider transformationSlider; // Slider para a transforma��o


    private float cooldownDash = 0.3f;

    private void Start()
    {
        transformationSlider.maxValue = transformationDuration; // Define o valor m�ximo do slider
        transformationSlider.value = transformationDuration; // Define o valor m�ximo do slider
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
        //Transforma��o
        if (transformationState == TransformationState.Ready &&
            Input.GetKey(KeyCode.LeftShift) &&
            Input.GetKeyDown(KeyCode.Z) && canAct)
        {
            Debug.Log("Ar");
            canAct = false;
            transformationState = TransformationState.Transforming;
            timer = transformationDuration;

            if (unit.PlayerController.currentElement != ElementType.Air)
            {
                Debug.Log("AirTransformation()");
                Debug.Log("Ativando o novo estado de transforma��o do Ar");
                unit.PlayerController.currentElement = ElementType.Air;
                unit.ChangeState<AirTransformationState>();
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.X) && canTransform && canAct)
        {
            canAct = false;
            canTransform = false;
            //EarthTransformation(); (ainda n�o existe)
        }
        // Reseta a transforma��o ap�s o tempo definido
        if (!canTransform)
        {
            transformationTimer -= Time.deltaTime;
            transformationSlider.value = transformationTimer; // Atualiza o slider com o tempo restante
            if (transformationTimer <= 0.01)
            {
                unit.PlayerController.currentElement = ElementType.Default; // Muda para o estado de transforma��o padr�o
            }
        }
        if(transformationTimer <= 0.01)
        {
            chargingTimer += Time.deltaTime * 3; // Incrementa o temporizador de carregamento
            transformationSlider.value = chargingTimer; // Atualiza o slider com o tempo restante
            if (transformationTimer >= 15f)
            {
                Debug.Log("J� pode se transformar novamente");
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
        //Ainda n�o existe
    }


}

public enum TransformationState
{
    Ready,
    Transforming,
    Cooldown
}