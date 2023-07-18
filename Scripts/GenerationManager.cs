using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public GameObject baseRoom;
    public List<RoomData> roomsByOrder = new List<RoomData>();
    int currentRoom = 0;

    public void GenerateRoom(Vector3 locat, Vector3 rotat)
    {
        if (currentRoom >= roomsByOrder.Count)
        {
            SpaceRoom room = Instantiate(roomsByOrder[roomsByOrder.Count - 1].room, locat, Quaternion.identity).GetComponent<SpaceRoom>();
            room.gameObject.transform.eulerAngles = rotat;
            room.Init(roomsByOrder[roomsByOrder.Count - 1]);
        }
        else
        {
            SpaceRoom room = Instantiate(roomsByOrder[currentRoom].room, locat, Quaternion.identity).GetComponent<SpaceRoom>();
            room.gameObject.transform.eulerAngles = rotat;
            room.Init(roomsByOrder[currentRoom]);
        }
        currentRoom++;
    }
}
