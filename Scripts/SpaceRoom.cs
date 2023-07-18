using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRoom : MonoBehaviour
{
    List<EnemyController> enemies = new List<EnemyController>();

    public SpaceDoor exitDoor;
    public Transform roomCornerLower;
    public Transform roomCornerUpper;

    bool active = false;
    public bool cleared = false;

    public void Init(RoomData dat)
    {
        for (int i = 0; i < dat.roomEnemies.Count; i++)
        {
            int min = dat.minEnemies[i];
            int max = dat.maxEnemies[i];

            int enemyCount = UnityEngine.Random.Range(min, max);
            GameObject prefab = dat.roomEnemies[i];
            SpawnEnemies(prefab, enemyCount);
        }

        if (enemies.Count > 0)
        {
            active = true;
        }
    }

    private void Start()
    {
        exitDoor.active = false;
    }

    private void SpawnEnemies(GameObject enemy, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = new Vector3();

            spawnPos.x = UnityEngine.Random.Range(roomCornerLower.transform.position.x, roomCornerUpper.transform.position.x);
            spawnPos.y = UnityEngine.Random.Range(roomCornerLower.transform.position.y, roomCornerUpper.transform.position.y);
            spawnPos.z = UnityEngine.Random.Range(roomCornerLower.transform.position.z, roomCornerUpper.transform.position.z);

            enemies.Add(Instantiate(enemy, spawnPos, Quaternion.identity).GetComponent<EnemyController>());
        }
    }

    private void Update()
    {
        if (!active)
        {
            return;
        }
        bool enemyStillActive = false;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                enemyStillActive = true;
                break;
            }
        }

        if (!enemyStillActive)
        {
            cleared = true;
            exitDoor.active = true;
            active = false;
        }
    }
}
