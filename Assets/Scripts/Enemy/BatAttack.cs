using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public float lifeTime = 2;
    public float impulse = 50f;
    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * impulse, ForceMode.Impulse);
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.Self);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
