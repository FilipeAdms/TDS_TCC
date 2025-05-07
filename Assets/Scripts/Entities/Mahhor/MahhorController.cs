using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahhorController : MonoBehaviour
{
    private PlayerStateMachine unit;
    private StatusComponent status;
    [SerializeField] private MahhorBars mahhorBars;

    private void Start()
    {
        status = GetComponent<StatusComponent>();
        unit = GetComponent<PlayerStateMachine>();

        mahhorBars.SetMaxHealth(status.maxHealth);
        mahhorBars.SetHealth(status.currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
