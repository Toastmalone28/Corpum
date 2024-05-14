using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float lifeTime = 2;
    public float impulse = 3f;
    public GameObject player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(((player.transform.position - transform.position).normalized) * impulse, ForceMode.Impulse);
        Destroy(gameObject, lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
