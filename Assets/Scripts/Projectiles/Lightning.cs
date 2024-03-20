using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Lightning : Projectile
{
    public LightningStrike effect;
    private Collider firstCollision;
    private Quaternion Q;

    public GameObject SparkBall;
    public GameObject ChainLightning;


    protected override void Start()
    {
        base.Start();
        Q = transform.rotation * Quaternion.Euler(0, 90, 90);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().DealDamage(effect.damage);
            firstCollision = collision.gameObject.GetComponent<EnemyBehaviour>().GetAbilityTrigger().GetComponent<Collider>();
            StartCoroutine(Explode(collision));
            LightningAnimation(collision.gameObject);
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
                LightningAnimation(collider.gameObject);
                ChainAnimation(firstCollision.gameObject, collider.gameObject);

                collider.GetComponentInParent<EnemyBehaviour>().DealDamage(effect.damage / 2);
                targetsHit++;
            }
        }
        Destroy(gameObject);
    }

    private void ChainAnimation(GameObject start, GameObject target)
    {
        Vector3 targetPoint = target.transform.position;
        Vector3 startPoint = start.transform.position;
        Vector3 angle = targetPoint - startPoint;
        Vector3 offset = new(0, 1, 0);

        GameObject attack = Instantiate(ChainLightning, startPoint, Q);

        float distance = Vector3.Distance(startPoint, targetPoint);

        attack.transform.rotation = Quaternion.LookRotation(angle, Vector3.up) * Quaternion.Euler(0, 90, 90);
        attack.transform.position = attack.transform.position + ((targetPoint - attack.transform.position) / 2) + offset;
        attack.transform.localScale = new Vector3(attack.transform.localScale.x, distance, attack.transform.localScale.z);

        Destroy(attack, 0.5f);
    }

    private void LightningAnimation(GameObject gameObject)
    {
        GameObject animation = Instantiate(SparkBall, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(animation, 3);
    }
}
