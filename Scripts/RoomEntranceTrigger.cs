using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntranceTrigger : MonoBehaviour
{
    public SpaceRoom currentRoom;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("Combo Canvas").GetComponent<ComboManager>().OnRoomEntered(currentRoom);
        }
    }
}
