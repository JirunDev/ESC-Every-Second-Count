using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;
    private int health;
    private int healthMax;
    public HealthSystem(int _healthMax)
    {
        healthMax = _healthMax;
        health = _healthMax;
    }

    public int GetHealth()
    {
        return health;
    }
    
    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (health <= 0)
        {
            health = 0;
        }
    }

    public void OnHeal(int healAmount)
    {
        health += healAmount;
        OnHealed?.Invoke(this, EventArgs.Empty);
        if (health > healthMax) health = healthMax;
    }

    public bool isDead() {
        return (health <= 0);
    }

}
