using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_Random : EnemyController
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
        int choice = UnityEngine.Random.Range(0, 6);
        if (choice == 0)
        {
            rb.AddForce(visual.transform.up * moveForce);
        }
        else if (choice == 1)
        {
            rb.AddForce(visual.transform.up * -1 * moveForce);
        }
        else if (choice == 2)
        {
            rb.AddForce(visual.transform.right * -1 * moveForce);
        }
        else if (choice == 3)
        {
            rb.AddForce(visual.transform.right * moveForce);
        }
        else if (choice == 4)
        {
            rb.AddForce(visual.transform.forward * moveForce);
        }
        else if (choice == 5)
        {
            rb.AddForce(visual.transform.forward * -1 * moveForce);
        }
        timer = 0;
        waitTime = UnityEngine.Random.Range(3, 10);
    }
}
