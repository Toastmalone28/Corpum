using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.ProBuilder;

[CreateAssetMenu(menuName = "Effects/LightningStrike")]
public class LightningStrike : Effect
{
    public GameObject projectile;
    public Transform barrel;
    public GameObject orientation;

    public float damage;
    public float explosionRadius;
    public int maxTargets;
    public float dischargeTimer;

    public void Awake()
    {
        //barrel = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GunBehaviour>().barrel;
        //orientation = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GunBehaviour>().orientation;
    }
    public override void Apply()
    {
        if(barrel == null)
        {
            barrel = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GunBehaviour>().barrel;
        }
        if(orientation == null)
        {
            orientation = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GunBehaviour>().orientation;
        }
        Instantiate(projectile, barrel.position, orientation.transform.rotation);
    }
}
