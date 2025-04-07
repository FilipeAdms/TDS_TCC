using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Classe est�tica para que n�o seja necessa�rio criar objetos para chamar os m�todos
public static class SaveSystem
{
    // Este m�todo vai procurar o caminho em que o arquivo est� salvo e colocar o .json no final para poder salvar os dados.
    private static string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName + ".json";
    }

    // M�todo para salvar os dados do persoangem
    public static void SaveCharacter(CharacterData character, string fileName)
    {
        // Converte o objeto characterdata para uma string json, true serve para melhorar a formata��o e deixar mais leg�vel
        string json = JsonUtility.ToJson(character, true);
        // Salva o json no arquivo "fileName", criando o arquivo com o nome fornecido no caminho gerado por GetPath
        File.WriteAllText(GetPath(fileName), json);
        // Mostrando onde foi salvo essa coisa
        Debug.Log("Personagem salvo em: " + GetPath(fileName));
    }

    // Esse m�todo carrega os dados de um personagem a partir do arquivo JSON

    public static CharacterData LoadCharacter(string fileName)
    {
        // Pega o caminho completo do arquivo
        string path = GetPath(fileName);

        //verifica se o arquivo realmente existe, se n�o existir, ele retorna null
        if (File.Exists(path))
        {
            // l� o arquivo json e guarda ele na vari�vel json como uma string
            string json = File.ReadAllText(path);
            // Converte a string json de volta para um objeto do tipo CharacterData
            CharacterData character = JsonUtility.FromJson<CharacterData>(json);
            Debug.Log("Personagem carregado: " + character.name);
            return character;
        }else
        {
            Debug.LogWarning("Arquivo n�o encontrado: " + path);
            return null;
        }
    }
}
