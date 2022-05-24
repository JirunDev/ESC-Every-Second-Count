using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public enum Stage
    {
        WaitToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }

    private Stage stage;

    [SerializeField] private Boss1Logic boss1;
    
    private void Awake()
    {
        stage = Stage.WaitToStart;
    }

    private void Start()
    {
        boss1.GetHealthSystem().OnDamaged += BossBattle_OnDamaged;
    }
    private void StartBattle()
    {
        Debug.Log("Start battle");
        StartNextStage();
    }
    private void BossBattle_OnDamaged(object sender, System.EventArgs e)
    {
        // Boss take Damage
        switch (stage)
        {
            case Stage.Stage_1:
                if (boss1.GetHealthSystem().GetHealthPercent() < 0.7f)
                {
                    // Enemy health < 70 %
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:
                if (boss1.GetHealthSystem().GetHealthPercent() < 0.3f)
                {
                    // Enemy health < 30 %
                    StartNextStage();
                }
                break;
        }
    }

    private void BossBattle_OnDead(object sender, System.EventArgs e)
    {
        // Boss Dead!
        Debug.Log("Boss Battle Over!");
    }

    private void StartNextStage()
    {
        switch (stage)
        {
            case Stage.WaitToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;
        }
        Debug.Log("Start next stage : " + stage);
    }

    private void SpawnEnemies()
    {

    }

    private void DestroyAllEnemies()
    {

    }
}
