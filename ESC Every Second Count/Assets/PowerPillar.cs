using System;
using UnityEngine;

public class PowerPillar : MonoBehaviour
{
    private HealthBar healthBar;
    private HealthSystem healthSystem;

    [SerializeField] private int shieldHealth;

    [SerializeField] private GameObject ExplodeEffect;

    private static int pillarCount;

    public event EventHandler OnShieldDestroyed;

    private void Awake()
    {
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        healthSystem = new HealthSystem(shieldHealth);
        healthBar.Setup(healthSystem);

        healthSystem.OnDamaged += OnShieldDamaged;
    }
    private void OnShieldDamaged(object sender, EventArgs e)
    {
        if (healthSystem.isDead())
        {
            Instantiate(ExplodeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            OnShieldDestroyed?.Invoke(this, EventArgs.Empty);
        }
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

}
