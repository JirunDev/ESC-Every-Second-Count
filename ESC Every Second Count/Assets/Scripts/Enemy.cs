using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Collider MonsterCollider;
    private Collider[] RagdollColliders;
    private Animator anim;

    private void Awake()
    {
        MonsterCollider = GetComponent<Collider>();
        RagdollColliders = GetComponentsInChildren<Collider>();
        anim = GetComponentInChildren<Animator>();

        ActivateRagdoll(false);
    }

    private void ActivateRagdoll(bool Status)
    {
        foreach (Collider col in RagdollColliders)
        {
            col.enabled = Status;
        }

        MonsterCollider.enabled = !Status;
        anim.enabled = !Status;
        GetComponent<Rigidbody>().useGravity = !Status;
    }

    public void KillEnemy(Vector3 ExplosionPosition)
    {
        ActivateRagdoll(true);

        foreach (Collider col in RagdollColliders)
        {
            col.GetComponent<Rigidbody>().AddExplosionForce(40f, ExplosionPosition, 3f, 3f, ForceMode.VelocityChange);
        }
    }
}
