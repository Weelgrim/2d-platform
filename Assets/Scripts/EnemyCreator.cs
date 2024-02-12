using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField]private float setTimeToSpawn;
    private float timeToSpawn;
    private Vector3 _transform;

    public GameObject objectToSpawn;

    private void Start()
    {
        timeToSpawn = setTimeToSpawn;
        _transform = transform.position;
    }

    private void Update()
    {
        timeToSpawn -= Time.deltaTime;

        if(timeToSpawn <= 0)
        {
            SpawnEnemy();
            timeToSpawn = setTimeToSpawn;
        }

    }
    void SpawnEnemy()
    {
        GameObject newObject = Instantiate(objectToSpawn, _transform, transform.rotation);
    }
}
