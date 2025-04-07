using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Classe estática para que não seja necessa´rio criar objetos para chamar os métodos
public static class SaveSystem
{
    // Este método vai procurar o caminho em que o arquivo está salvo e colocar o .json no final para poder salvar os dados.
    private static string GetPath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName + ".json";
    }

    // Método para salvar os dados do persoangem
    public static void SaveCharacter(CharacterData character, string fileName)
    {
        // Converte o objeto characterdata para uma string json, true serve para melhorar a formatação e deixar mais legível
        string json = JsonUtility.ToJson(character, true);
        // Salva o json no arquivo "fileName", criando o arquivo com o nome fornecido no caminho gerado por GetPath
        File.WriteAllText(GetPath(fileName), json);
        // Mostrando onde foi salvo essa coisa
        Debug.Log("Personagem salvo em: " + GetPath(fileName));
    }

    // Esse método carrega os dados de um personagem a partir do arquivo JSON

    public static CharacterData LoadCharacter(string fileName)
    {
        // Pega o caminho completo do arquivo
        string path = GetPath(fileName);

        //verifica se o arquivo realmente existe, se não existir, ele retorna null
        if (File.Exists(path))
        {
            // lê o arquivo json e guarda ele na variável json como uma string
            string json = File.ReadAllText(path);
            // Converte a string json de volta para um objeto do tipo CharacterData
            CharacterData character = JsonUtility.FromJson<CharacterData>(json);
            Debug.Log("Personagem carregado: " + character.name);
            return character;
        }else
        {
            Debug.LogWarning("Arquivo não encontrado: " + path);
            return null;
        }
    }
}
