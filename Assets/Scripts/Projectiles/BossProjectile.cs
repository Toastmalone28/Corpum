using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new possible Attacks", menuName = "enemy Attacks/possible Attacks")]
public class BossProjectile : ScriptableObject
{
    
    public GameObject bullet;
    public float speed = 45;
    public float lifeTime = 2;
    public float impulse = 500f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * impulse, ForceMode.Impulse);
        Destroy(bullet, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(bullet);
    }
}
