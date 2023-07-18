using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Player location (found in start)
    private Transform playerTransform;

    [Header("Enemy Controller Variables")]

    public GameObject visual;
    public int damage = 1;
    public GameObject gore;

    private void Start()
    {
        //Find the player
        playerTransform = GameObject.Find("Player").transform;
        StartSecondary();
    }

    public virtual void StartSecondary()
    {

    }

    private void Update()
    {
        //Constantly look towards player
        visual.transform.LookAt(playerTransform);
        UpdateSecondary();
    }

    public virtual void UpdateSecondary()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().Damage();
        }
    }

    public void OnDeath()
    {
        Instantiate(gore.gameObject, visual.transform.position, visual.transform.rotation);
        Destroy(this.gameObject);
    }
}
