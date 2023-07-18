using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="newRoom", menuName="New Room")]
public class RoomData : ScriptableObject
{
    public List<GameObject> roomEnemies = new List<GameObject>();
    public List<int> minEnemies = new List<int>();
    public List<int> maxEnemies = new List<int>();
    public GameObject room;
}
