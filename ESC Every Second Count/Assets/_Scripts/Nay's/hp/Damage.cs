using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    /*public GameObject bodypart;

    RaycastHit hitRay;*/

    public int maxHealth = 100;
    int currentHealth;

    public HealthBar healthBar;

    [SerializeField] private Transform player;
    public GameObject respawnPoint;
    public GameObject respawnedPrompt;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Respawn();
            currentHealth = maxHealth;
        }
            
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint.transform.position;
        currentHealth = maxHealth;

        StartCoroutine(RespawnPrompt());
        currentHealth = maxHealth;

        healthBar.valueText.text = maxHealth.ToString();
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint.transform.position = newPosition;
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
        respawnPoint.SetActive(true);
        respawnedPrompt.SetActive(true);
        yield return new WaitForSeconds(5);
        respawnedPrompt.SetActive(false);
        respawnPoint.SetActive(false);
    }
}
