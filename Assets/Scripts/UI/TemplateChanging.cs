using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TemplateChanging : MonoBehaviour
{
    // Sprites do personagem para cada elemento
    [SerializeField] private Image defaultFormImage;  // Forma normal
    [SerializeField] private Image airFormImage;      // Forma do elemento Ar
    [SerializeField] private Image earthFormImage;    // Forma do elemento Terra

    // Tempo para fazer a transição entre formas
    [SerializeField] private float transitionDuration = 1f;

    // Inicializa com a forma padrão visível e as outras invisíveis
    private void Start()
    {
        SetImageAlpha(defaultFormImage, 1f); // Visível
        SetImageAlpha(airFormImage, 0f);     // Invisível
        SetImageAlpha(earthFormImage, 0f);   // Invisível
    }

    // Ativa a forma de Ar com transição
    public void AirTransformationTemplate()
    {
        StopAllCoroutines(); // Para qualquer transição anterior
        StartCoroutine(FadeBetweenForms(defaultFormImage, airFormImage));
    }

    // Ativa a forma de Terra com transição
    public void EarthTransformationTemplate()
    {
        StopAllCoroutines(); // Para qualquer transição anterior
        StartCoroutine(FadeBetweenForms(defaultFormImage, earthFormImage));
    }

    // Retorna para a forma padrão (normal) com transição
    public void DefaultTransformationTemplate()
    {
        StopAllCoroutines(); // Para qualquer transição anterior

        // Verifica qual forma está ativa e volta para a padrão
        if (airFormImage.color.a > 0.5f)
        {
            StartCoroutine(FadeBetweenForms(airFormImage, defaultFormImage));
        }
        else if (earthFormImage.color.a > 0.5f)
        {
            StartCoroutine(FadeBetweenForms(earthFormImage, defaultFormImage));
        }
    }

    // Faz a transição suave entre duas imagens (fade out e fade in)
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
            yield return null; // Espera o próximo frame
        }

        // Garante o estado final correto
        SetImageAlpha(fromImage, 0f);
        SetImageAlpha(toImage, 1f);
    }

    // Define a transparência (alpha) de uma imagem sem alterar as outras cores
    private void SetImageAlpha(Image image, float newAlpha)
    {
        Color imageColor = image.color;
        imageColor.a = newAlpha;
        image.color = imageColor;
    }
}
