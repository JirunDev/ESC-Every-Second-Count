using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    //Assignables
    private Rigidbody rb;
    [SerializeField] private GameObject explosion;
    [SerializeField] private LayerMask whatIsPlayer;

    //Stats
    [Range(0f, 1f)]
    [SerializeField] private float bounciness;
    [SerializeField] private bool useGravity;

    //Damage
    [SerializeField] private int explosionDamage;
    [SerializeField] private float explosionRange;

    //Lifetime
    [SerializeField] private int maxCollisions;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Setup();
    }

    private void Update()
    {
        if (collisions > maxCollisions) Explode();

        //Count down lifetime
        maxLifeTime -= Time.deltaTime;
        if (maxLifeTime <= 0f) Explode();

    }
    private void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        // Check player in range
        Collider[] players = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        for (int i = 0; i < players.Length; i++) // if there is multiplayer
        {
            // Player take damage ******** (include later)
            player.GetComponent<Damage>().TakeDamage(10);
        }

        Invoke(nameof(Delay), 0.01f);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Count up collisions
        collisions++;

        //Explode if collide with enemy and explodeOnTouch is activated
        if (collision.collider.CompareTag("Player") && explodeOnTouch) Explode();
    }
    private void Setup()
    {
        // create new Physics material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
