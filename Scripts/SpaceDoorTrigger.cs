using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDoorTrigger : MonoBehaviour
{
    public bool open = true;
    public SpaceDoor mainDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (open)
            {
                mainDoor.Open();
            }
            else
            {
                mainDoor.Close();
            }
        }
    }

}
