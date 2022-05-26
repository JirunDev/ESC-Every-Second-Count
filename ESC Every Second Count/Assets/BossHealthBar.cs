using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    private BossHealthSystem healthSystem;

    public void Setup(BossHealthSystem _healthSystem)
    {
        healthSystem = _healthSystem;
    }

    private void Update()
    {
        Transform healthBar = transform.Find("Bar");
        healthBar.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}
