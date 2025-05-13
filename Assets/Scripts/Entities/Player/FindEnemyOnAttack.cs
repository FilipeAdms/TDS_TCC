using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemyOnAttack : MonoBehaviour
{
    private Collider2D[] findEnemy;
    private StatusComponent status;
    [SerializeField] private LayerMask layerMask;
    private Transform unitPosition;
    private float posX;
    private float posY;

    // Start is called before the first frame update
    void Start()
    {
        unitPosition = GetComponent<Transform>();
        status = GetComponent<StatusComponent>();
    }

    public void FindEnemy()
    {
        switch (DirectionUtils.currentDirection)
        {
            case Directions.Right:
                posX = unitPosition.position.x + 0.75f;
                posY = unitPosition.position.y;
                break;
            case Directions.Left:
                posX = unitPosition.position.x - 0.75f;
                posY = unitPosition.position.y;
                break;
            case Directions.Up:
                posX = unitPosition.position.x;
                posY = unitPosition.position.y + 0.25f;
                break;
            case Directions.Down:
                posX = unitPosition.position.x;
                posY = unitPosition.position.y -0.5f;
                break;
        }
        
        // Ativa detecção de inimigos
        findEnemy = Physics2D.OverlapCircleAll(new Vector2(posX, posY), 0.5f, layerMask); // Verifica se há inimigos dentro do raio de 1 unidade

        foreach (Collider2D enemy in findEnemy)
        {
            if(enemy.name == "Mahhor")
            {
                enemy.GetComponent<MahhorBars>().TakeDamage(status.currentAttackDamage);
            }
            else
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(3);
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        switch (DirectionUtils.currentDirection)
        {
            case Directions.Right:
                Gizmos.DrawWireSphere(new Vector2(posX, posY), 0.50f);

                break;
            case Directions.Left:
                Gizmos.DrawWireSphere(new Vector2(posX, posY), 0.50f);

                break;
            case Directions.Up:
                Gizmos.DrawWireSphere(new Vector2(posX, posY), 0.5f);

                break;
            case Directions.Down:
                Gizmos.DrawWireSphere(new Vector2(posX, posY), 0.5f);

                break;
        }
    }
}
