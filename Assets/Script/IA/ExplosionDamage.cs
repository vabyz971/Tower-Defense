
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{

    public float explosionRadius = 0;
    public int damage = 20;


    void Start()
    {
        if (explosionRadius > 0)
            Explode();
        else
            return;
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Turret")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {

        Turret t = enemy.GetComponent<Turret>();

        if (t != null)
        {
            t.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
