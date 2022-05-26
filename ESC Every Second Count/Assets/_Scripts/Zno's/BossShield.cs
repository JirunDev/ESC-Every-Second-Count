using System;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    private PowerPillar[] powerPillars;

    private int shieldDestroyedCount;
    [SerializeField] private int shieldNumber;

    public event EventHandler OnBossShieldDestroyed;
    private void Start()
    {
        powerPillars = GameObject.FindObjectsOfType<PowerPillar>();
        foreach (PowerPillar powerPillar in powerPillars)
        {
            powerPillar.OnShieldDestroyed += BossShield_OnShieldDestroyed;
        }
    }

    private void BossShield_OnShieldDestroyed(object sender, EventArgs e)
    {
        shieldDestroyedCount++;
        if (shieldDestroyedCount == shieldNumber)
        {
            OnBossShieldDestroyed?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
    }
}
