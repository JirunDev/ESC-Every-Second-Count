using System;

public class HealthSystem
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    private int health;
    private int healthMax;
    public HealthSystem(int _healthMax)
    {
        healthMax = _healthMax;
        health = healthMax;
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
        if (OnDamaged != null) OnDamaged(this, EventArgs.Empty);
        if (health < 0) health = 0;
    }

    public void OnHeal(int healAmount)
    {
        health += healAmount;
        if (OnHealed != null) OnHealed(this, EventArgs.Empty);
        if (health > healthMax) health = healthMax;
    }
}
