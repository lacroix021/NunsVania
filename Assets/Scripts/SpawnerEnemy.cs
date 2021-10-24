using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    GameObject enemyInstance;

    private void OnEnable()
    {
        if (!enemyInstance)
        {
            enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemyInstance.name = enemyPrefab.name;
            enemyInstance.transform.parent = transform.parent;
        }
    }

    private void OnDisable()
    {
        if (enemyInstance)
        {
            enemyInstance.transform.position = transform.position;
        }
    }
}
