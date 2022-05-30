using System;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    private BossShield bossShield;

    public event EventHandler OnShieldClosed;

    private void Awake()
    {
        bossShield = transform.Find("Shield Field").gameObject.GetComponent<BossShield>();
        bossShield.OnBossShieldDestroyed += OnBossShieldClosed;
    }

    private void OnBossShieldClosed(object sender, EventArgs e)
    {
        OnShieldClosed?.Invoke(this, EventArgs.Empty);
    }
    
    
}
