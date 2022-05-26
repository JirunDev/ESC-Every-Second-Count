using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject Barrel, Explosion;

    private AudioSource source;

    [SerializeField]
    private float range;

    private void Awake()
    {
        Barrel.SetActive(true);
        Explosion.SetActive(false);

        source = GetComponent<AudioSource>();
    }

    public void Explode()
    {
        Barrel.SetActive(false);
        Explosion.SetActive(true);

        source.Play();

        Collider[] enemies = Physics.OverlapSphere(transform.position, range);

        foreach (Collider enemy in enemies)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().KillEnemy(transform.position);
            }
        }

        this.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
