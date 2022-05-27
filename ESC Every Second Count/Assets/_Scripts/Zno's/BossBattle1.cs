using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBattle1 : MonoBehaviour
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
    [SerializeField] private ColliderTrigger colliderTrigger;

    [Header("SpawningEnemies")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnRate;
    private List<Enemy> SpawnEnemyList = new List<Enemy>();

    [Tooltip("Enemy which the boss will spawn in the battlefield")]
    [SerializeField] private GameObject[] SubEnemies;

    [SerializeField] private GameObject shield_1;
    [SerializeField] private GameObject shield_2;

    [SerializeField] private Toggle killBossToggle;
    public int maxEnemy = 15;
    
    private void Awake()
    {
        stage = Stage.WaitToStart;

        boss1.GetHealthSystem().OnDamaged += BossBattle_OnDamaged;
        colliderTrigger.OnPlayerEnterTrigger += ColliderTrigger_OnPlayerEnterTrigger;
        shield_1.GetComponent<ShieldManager>().OnShieldClosed += BossBattle_OnContinueAttack;
        shield_2.GetComponent<ShieldManager>().OnShieldClosed += BossBattle_OnContinueAttack;

        shield_1.SetActive(false);
        shield_2.SetActive(false);
    }

    private void ColliderTrigger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTrigger_OnPlayerEnterTrigger;
    }
    private void StartBattle()
    {
        Debug.Log("Start battle");
        InvokeRepeating(nameof(SpawnEnemies), 1, spawnRate);
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

        if (boss1.GetHealthSystem().isDead())
        {
            DestroyAllEnemies();
            Debug.Log("Boss Battle Over!");
            boss1.DestroySelf();

            //Toggle
            killBossToggle.isOn = true;
        }
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
                ActiveShield1();
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                ActiveShield2();
                break;
        }
        Debug.Log("Start next stage : " + stage);
    }

    private void ActiveShield1()
    {
        shield_1.SetActive(true);
        BossBattle_OnStopAttack();
    }

    private void ActiveShield2()
    {
        shield_2.SetActive(true);
        BossBattle_OnStopAttack();
    }
    private void SpawnEnemies()
    {
        if (SubEnemies != null && SpawnEnemyList.Count < maxEnemy)
        {
            Transform spawnPos = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Enemy spawnedEnemy = Instantiate(SubEnemies[Random.Range(0,SubEnemies.Length)], spawnPos.position, Quaternion.identity).GetComponent<Enemy>();
            SpawnEnemyList.Add(spawnedEnemy);
        }
    }

    private void BossBattle_OnStopAttack()
    {
        boss1.StopAttack();
    }

    private void BossBattle_OnContinueAttack(object sender, System.EventArgs e)
    {
        boss1.Active();
    }

    private void DestroyAllEnemies()
    {
        foreach (Enemy enemy in SpawnEnemyList)
        {
            if (enemy != null)
            {
                if (!enemy.GetComponent<Enemy>().GetHealthSystem().isDead())
                {
                    enemy.KillEnemy();
                }
            }
        }
        SubEnemies = null;
    }
}
