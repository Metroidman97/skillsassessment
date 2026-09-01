using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject explosion;
    private GameObject target;
    private Cannon cannon;

    private float speed = 30f;
    private float rotateSpeed = 180f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Target");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Target")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            cannon.MissileHit();
            Destroy(this.gameObject);
        }
    }

    private void Move()
    {
        //transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (target == null)
        {
            rb.velocity = transform.forward * speed;
            return;
        }

        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

        Vector3 newForward = Vector3.RotateTowards(transform.forward, directionToTarget, rotateSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newForward);

        rb.velocity = transform.forward * speed;
    }
}
