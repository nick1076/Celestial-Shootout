using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityManager : MonoBehaviour
{
    [Header("Physics Components")]
    public Rigidbody playerPhysics;

    [Header("Physics Settings")]
    public float speedLimit;
    public float velocityDecay;

    private void Update()
    {
        Vector3 playerVel = playerPhysics.velocity;

        if (playerVel.x > speedLimit)
        {
            playerVel.x = speedLimit;
        }

        if (playerVel.y > speedLimit)
        {
            playerVel.y = speedLimit;
        }

        if (playerVel.z > speedLimit)
        {
            playerVel.z = speedLimit;
        }

        if (playerVel.x > 0)
        {
            if (playerVel.x - velocityDecay < 0)
            {
                playerVel.x = 0;
            }
            else
            {
                playerVel.x -= velocityDecay;
            }
        }
        else
        {
            if (playerVel.x + velocityDecay > 0)
            {
                playerVel.x = 0;
            }
            else
            {
                playerVel.x += velocityDecay;
            }
        }

        if (playerVel.y > 0)
        {
            if (playerVel.y - velocityDecay < 0)
            {
                playerVel.y = 0;
            }
            else
            {
                playerVel.y -= velocityDecay;
            }
        }
        else
        {
            if (playerVel.y + velocityDecay > 0)
            {
                playerVel.y = 0;
            }
            else
            {
                playerVel.y += velocityDecay;
            }
        }

        if (playerVel.z > 0)
        {
            if (playerVel.z - velocityDecay < 0)
            {
                playerVel.z = 0;
            }
            else
            {
                playerVel.z -= velocityDecay;
            }
        }
        else
        {
            if (playerVel.z + velocityDecay > 0)
            {
                playerVel.z = 0;
            }
            else
            {
                playerVel.z += velocityDecay;
            }
        }

        playerPhysics.velocity = playerVel;
    }
}
