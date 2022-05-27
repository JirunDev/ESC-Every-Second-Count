using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject bulletNormal;
    public GameObject bulletExp;
    /*public GameObject bodypart;

    RaycastHit hitRay;*/

    public int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;

    [SerializeField] private Transform player;
    private Vector3 respawnPoint;
    public GameObject respawnedPrompt;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        respawnPoint = player.transform.position;
    }

    void FixedUpdate()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth - 1;
        }
    }
    /*    var ray = new Ray(this.transform.position, this.tramsform.forward)
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000 ))
            hit.collider.gameObject
    }*/

    void OnCollisionEnter(Collision dataFromCollision)
    {
        if (dataFromCollision.gameObject == bulletNormal)
        {
            TakeDamage(5);
        }
        if (dataFromCollision.gameObject == bulletExp)
        {
            TakeDamage(15);
        }

    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth == 0)
        {
            Respawn();
            currentHealth = maxHealth;
        }
            
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint;
        currentHealth = maxHealth;

        StartCoroutine(RespawnPrompt());
        currentHealth = maxHealth;

        healthBar.valueText.text = maxHealth.ToString();
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Healing")
        {
            TakeDamage(-1);
        }
        if (other.name == "Damaging")
        {
            TakeDamage(1);
        }
    }

    IEnumerator RespawnPrompt()
    {
        respawnedPrompt.SetActive(true);
        yield return new WaitForSeconds(5);
        respawnedPrompt.SetActive(false);
    }
}
