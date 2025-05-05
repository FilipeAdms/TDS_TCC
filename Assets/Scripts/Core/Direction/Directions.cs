using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enum que representa as dire��es poss�veis
public enum Directions
{
    Up,
    Down,
    Left,
    Right,
    None
}

// Classe que cont�m m�todos para manipular dire��es
public static class DirectionUtils
{
    public static Directions currentDirection = Directions.Right; // Dire��o atual
    // M�todo para obter a dire��o entre dois objetos
    public static Directions GetObjectDirection(Transform referenceTransform, Transform targetTransform)
    {
        if (referenceTransform == null || targetTransform == null)
        {
            return currentDirection = Directions.None;
        }

        // Calcula a diferen�a de posi��o entre os dois objetos
        Vector2 direction = (targetTransform.position - referenceTransform.position).normalized;

        // Verifica se os objetos est�o na mesma posi��o
        if (direction == Vector2.zero)
        {
            return currentDirection = Directions.None;
        }

        // Determina a dire��o dominante
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x >= 0 ? currentDirection = Directions.Right : currentDirection = Directions.Left;
        }
        else
        {
            return direction.y > 0 ? currentDirection = Directions.Up : currentDirection = Directions.Down;
        }
    }

     // Atualiza a dire��o com base no movimento
    public static Directions UpdateDirection( float hMove, float vMove)
    {
        if (Mathf.Abs(hMove) > Mathf.Abs(vMove))
        {
            // Movimento horizontal � dominante
            return hMove > 0 ? currentDirection = Directions.Right : currentDirection = Directions.Left;
        }
        else if (Mathf.Abs(vMove) > 0)
        {
            // Movimento vertical � dominante
            return vMove > 0 ? currentDirection = Directions.Up : currentDirection = Directions.Down;
        }
        else
        {
            // Nenhum movimento
            return currentDirection = Directions.None;
        }
    }
}


