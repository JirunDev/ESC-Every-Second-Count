using System;
using UnityEngine;
using UnityEngine.AI;

public class Boss1Logic : MonoBehaviour
{
    [SerializeField] private int health;
    private BossHealthSystem healthSystem;

    private NavMeshAgent agent;

    private Transform player;

    public BossHealthBar healthBar;

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

    private bool stopAttack = false;

    [Header("State Control")]
    //States
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        healthSystem = new BossHealthSystem(health);

        healthBar.Setup(healthSystem);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        // Check for in sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //States
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !stopAttack) AttackPlayer();
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
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

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
            Rigidbody rb = Instantiate(projectile, shotPoint.transform.position, Quaternion.LookRotation(player.transform.position, Vector3.up)).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * horizontalSpeed, ForceMode.Impulse);
            rb.AddForce(transform.up * verticalSpeed, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    public void StopAttack()
    {
        stopAttack = true;
    }

    public void Active()
    {
        stopAttack = false;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    //Display Sight and Attack Range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public BossHealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}