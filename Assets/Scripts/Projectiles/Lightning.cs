using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lightning : Projectile
{
    public LightningStrike effect;
    private Collider firstCollision;

    protected override void Start()
    {
        base.Start();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().DealDamage(effect.damage);
            firstCollision = collision.gameObject.GetComponent<EnemyBehaviour>().GetAbilityTrigger().GetComponent<Collider>();
            StartCoroutine(Explode(collision));
        }
    }

    private IEnumerator Explode(Collision collision)
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(effect.dischargeTimer);

        Vector3 explosionPoint = collision.contacts[0].point;
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, effect.explosionRadius);

        int targetsHit = 0;
        foreach (Collider collider in colliders)
        {
            if (targetsHit >= effect.maxTargets)
                break;
            if (collider == firstCollision)
                continue;
            if (collider.CompareTag("AbilityTrigger"))
            {
                collider.GetComponentInParent<EnemyBehaviour>().DealDamage(effect.damage / 2);
                targetsHit++;
            }
        }
        Destroy(gameObject);
    }
}
