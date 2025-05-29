using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TemplateChanging : MonoBehaviour
{
    // Sprites do personagem para cada elemento
    [SerializeField] private Image defaultFormImage;  // Forma normal
    [SerializeField] private Image airFormImage;      // Forma do elemento Ar
    [SerializeField] private Image earthFormImage;    // Forma do elemento Terra

    // Tempo para fazer a transi��o entre formas
    [SerializeField] private float transitionDuration = 1f;

    // Inicializa com a forma padr�o vis�vel e as outras invis�veis
    private void Start()
    {
        SetImageAlpha(defaultFormImage, 1f); // Vis�vel
        SetImageAlpha(airFormImage, 0f);     // Invis�vel
        SetImageAlpha(earthFormImage, 0f);   // Invis�vel
    }

    // Ativa a forma de Ar com transi��o
    public void AirTransformationTemplate()
    {
        StopAllCoroutines(); // Para qualquer transi��o anterior
        StartCoroutine(FadeBetweenForms(defaultFormImage, airFormImage));
    }

    // Ativa a forma de Terra com transi��o
    public void EarthTransformationTemplate()
    {
        StopAllCoroutines(); // Para qualquer transi��o anterior
        StartCoroutine(FadeBetweenForms(defaultFormImage, earthFormImage));
    }

    // Retorna para a forma padr�o (normal) com transi��o
    public void DefaultTransformationTemplate()
    {
        StopAllCoroutines(); // Para qualquer transi��o anterior

        // Verifica qual forma est� ativa e volta para a padr�o
        if (airFormImage.color.a > 0.5f)
        {
            StartCoroutine(FadeBetweenForms(airFormImage, defaultFormImage));
        }
        else if (earthFormImage.color.a > 0.5f)
        {
            StartCoroutine(FadeBetweenForms(earthFormImage, defaultFormImage));
        }
    }

    // Faz a transi��o suave entre duas imagens (fade out e fade in)
    private IEnumerator FadeBetweenForms(Image fromImage, Image toImage)
    {
        float timePassed = 0f;

        Color fromColor = fromImage.color;
        Color toColor = toImage.color;

        while (timePassed < transitionDuration)
        {
            float lerpFactor = timePassed / transitionDuration;

            SetImageAlpha(fromImage, Mathf.Lerp(1f, 0f, lerpFactor)); // Desaparece
            SetImageAlpha(toImage, Mathf.Lerp(0f, 1f, lerpFactor));   // Aparece

            timePassed += Time.deltaTime;
            yield return null; // Espera o pr�ximo frame
        }

        // Garante o estado final correto
        SetImageAlpha(fromImage, 0f);
        SetImageAlpha(toImage, 1f);
    }

    // Define a transpar�ncia (alpha) de uma imagem sem alterar as outras cores
    private void SetImageAlpha(Image image, float newAlpha)
    {
        Color imageColor = image.color;
        imageColor.a = newAlpha;
        image.color = imageColor;
    }
}
