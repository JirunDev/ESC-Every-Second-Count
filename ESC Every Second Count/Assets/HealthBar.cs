using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;

    public void Setup(HealthSystem _healthSystem)
    {
        healthSystem = _healthSystem;
    }

    private void Update()
    {
        Transform healthBar = transform.Find("Bar");
        healthBar.localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
    }
}
