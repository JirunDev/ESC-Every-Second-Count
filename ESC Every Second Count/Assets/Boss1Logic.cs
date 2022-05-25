using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Logic : MonoBehaviour
{
    public HealthBar healthBar;

    private HealthSystem healthSystem;
    [SerializeField] private int bossHealth = 1000;
    private enum State { 
        Rest,
        Attacking,
    }

    private void Awake()
    {
        healthSystem = new HealthSystem(bossHealth);
        healthBar.Setup(healthSystem);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
