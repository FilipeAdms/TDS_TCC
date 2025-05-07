using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorController : MonoBehaviour
{
    private MahhorStateMachine unit;
    private StatusComponent status;
    [SerializeField] private MahhorBars mahhorBars;

    [Header("Referências dos pontos de Patrulha")]
    public List<GameObject> patrolPoints = new List<GameObject>();

    private void Start()
    {
        status = GetComponent<StatusComponent>();
        unit = GetComponent<MahhorStateMachine>();

        mahhorBars.SetMaxHealth(status.maxHealth);
        mahhorBars.SetHealth(status.currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            unit.ChangeState<MahhorMoveState>();
        }
    }
}
