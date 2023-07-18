using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    public float bulletSpeed = 50;
    public bool doesDamage;
    public int damage;
    private void Start()
    {
        this.GetComponent<Rigidbody>().velocity = this.transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            OnImpact();
        }
        else
        {
            if (doesDamage)
            {
                other.gameObject.GetComponent<PlayerManager>().Damage(damage);
            }
        }
    }

    public void OnImpact()
    {
        Destroy(this.gameObject);
    }
}
