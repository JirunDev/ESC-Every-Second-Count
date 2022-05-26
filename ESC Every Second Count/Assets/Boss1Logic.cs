using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Logic : MonoBehaviour
{
    public BossHealthBar healthBar;

    private BossHealthSystem healthSystem;
    [SerializeField] private int bossHealth = 1000;
    private enum State { 
        Rest,
        Attacking,
    }

    private void Awake()
    {
        healthSystem = new BossHealthSystem(bossHealth);
        healthBar.Setup(healthSystem);
    }

    public BossHealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
