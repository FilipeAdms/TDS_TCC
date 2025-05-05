using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Enum que representa as direções possíveis
public enum Directions
{
    Up,
    Down,
    Left,
    Right,
    None
}

// Classe que contém métodos para manipular direções
public static class DirectionUtils
{
    public static Directions currentDirection = Directions.Right; // Direção atual
    // Método para obter a direção entre dois objetos
    public static Directions GetObjectDirection(Transform referenceTransform, Transform targetTransform)
    {
        if (referenceTransform == null || targetTransform == null)
        {
            return currentDirection = Directions.None;
        }

        // Calcula a diferença de posição entre os dois objetos
        Vector2 direction = (targetTransform.position - referenceTransform.position).normalized;

        // Verifica se os objetos estão na mesma posição
        if (direction == Vector2.zero)
        {
            return currentDirection = Directions.None;
        }

        // Determina a direção dominante
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x >= 0 ? currentDirection = Directions.Right : currentDirection = Directions.Left;
        }
        else
        {
            return direction.y > 0 ? currentDirection = Directions.Up : currentDirection = Directions.Down;
        }
    }

     // Atualiza a direção com base no movimento
    public static Directions UpdateDirection( float hMove, float vMove)
    {
        if (Mathf.Abs(hMove) > Mathf.Abs(vMove))
        {
            // Movimento horizontal é dominante
            return hMove > 0 ? currentDirection = Directions.Right : currentDirection = Directions.Left;
        }
        else if (Mathf.Abs(vMove) > 0)
        {
            // Movimento vertical é dominante
            return vMove > 0 ? currentDirection = Directions.Up : currentDirection = Directions.Down;
        }
        else
        {
            // Nenhum movimento
            return currentDirection = Directions.None;
        }
    }
}


