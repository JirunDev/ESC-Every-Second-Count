using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject bullet;
    /*public GameObject bodypart;

    RaycastHit hitRay;*/

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    [SerializeField] private Transform player;
    private bool isRespawning;
    private Vector3 respawnPoint;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        respawnPoint = player.transform.position;
    }

    /*void Update()
    {
        var ray = new Ray(this.transform.position, this.tramsform.forward)
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000 ))
            hit.collider.gameObject
    }*/

    void OnCollisionEnter(Collision dataFromCollision)
    {
        if (dataFromCollision.gameObject.name == "Bullet(Clone)")
        {
            TakeDamage(20);
        }
    }


    void TakeDamage(int damage)
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
        player.transform.position = respawnPoint;
        currentHealth = maxHealth;
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
