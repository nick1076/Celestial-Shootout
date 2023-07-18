using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Impulse : EnemyController
{
    float timer;
    float waitTime;

    [Header("Enemy Random Specific")]

    public Rigidbody rb;
    public float moveForce = 5000;

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
        }
    }

    private void Impulse()
    {
        rb.AddForce(visual.transform.forward * moveForce);
        timer = 0;
        waitTime = UnityEngine.Random.Range(2, 5);
    }
}
