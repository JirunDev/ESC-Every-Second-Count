using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health;
    private HealthSystem healthSystem;

    private NavMeshAgent agent;

    private Transform player;

    public HealthBar healthBar;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Patrolling")]
    //Patrol
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public float horizontalSpeed;
    public float verticalSpeed;
    public GameObject shotPoint;

    [Header("State Control")]
    //States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        healthSystem = new HealthSystem(health);
        healthSystem.OnDamaged += Enemy_OnDamaged;
        healthSystem.OnDead += Enemy_OnDead;

        healthBar.Setup(healthSystem);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Enemy_OnDamaged(object sender, System.EventArgs e) {
        if (healthSystem.isDead()) Destroy(gameObject);
    }

    public void Enemy_OnDead(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        // Check for in sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //States
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false; // Return to SearchWalkPoint();
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }
    private void ChasePlayer()
    {
        transform.LookAt(player);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Stop enemy from Patrolling or Chasing
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Attack!
            Rigidbody rb = Instantiate(projectile, shotPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * horizontalSpeed, ForceMode.Impulse);
            rb.AddForce(transform.up * verticalSpeed, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    //Display Sight and Attack Range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    public void KillEnemy()
    {
        healthSystem.Damage(10000);
    }

}
