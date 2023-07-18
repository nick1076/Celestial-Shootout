using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Fire : EnemyController
{
    float timer;
    float waitTime;

    [Header("Enemy Fire Specific")]

    public Rigidbody rb;
    public float moveForce = 7500;
    public GameObject tracer;
    public Transform fireOrigin;

    public override void StartSecondary()
    {
        base.StartSecondary();
        Impulse();
    }

    public override void UpdateSecondary()
    {
        base.UpdateSecondary();
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            Impulse();
            Fire();
        }
    }

    private void Impulse()
    {
        rb.AddForce(visual.transform.forward * moveForce);
        timer = 0;
        waitTime = UnityEngine.Random.Range(2, 5);
    }

    private void Fire()
    {
        Instantiate(tracer, fireOrigin.transform.position, fireOrigin.transform.rotation);
    }
}
