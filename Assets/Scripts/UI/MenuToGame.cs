using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuToGame : MonoBehaviour
{
    public Image imageFade;
    public RectTransform rectTransform; // Refer�ncia ao RectTransform do Image
    public RectTransform TecladoTransform; // Refer�ncia ao RectTransform do Image
    public RectTransform LetrasTransform; // Refer�ncia ao RectTransform do Image
    private float duracao = 1f; // Dura��o do fade in

    public void CarregarCena()
    {
        rectTransform.anchoredPosition = Vector2.zero; // Reseta a posi��o do RectTransform
        StartCoroutine(FadeIn("CampoArena"));
    }
    public IEnumerator FadeIn(string nomeDaCena)
    {
        // Come�a transparente
        Color color = imageFade.color;
        color.a = 0f;
        imageFade.color = color;

        float tempo = 0f;

        // Faz o fade in suavemente
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float alpha = Mathf.Clamp01(tempo / duracao);
            color.a = alpha;
            imageFade.color = color;
            yield return null; // Espera o pr�ximo frame
        }

        // Garante alpha no final
        color.a = 1f;
        imageFade.color = color;
        TecladoTransform.anchoredPosition = new Vector2(-4.95f, 133f); // Reseta a posi��o do RectTransform

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(nomeDaCena);
    }


}
