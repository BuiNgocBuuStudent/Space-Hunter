using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gamePrefabs;
    private float posX = 20.0f;
    private float posZ = -0.5f;
    private float startTime = 2.0f;
    private float repeatSpawnTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startTime, repeatSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnEnemy()
    {
        int index = Random.Range(0, gamePrefabs.Length);
        float randomPosY = Random.Range(-4.0f, 4.0f);
        Vector3 randomSpawnPos = new Vector3(posX, randomPosY, posZ);

        Instantiate(gamePrefabs[index], randomSpawnPos, gamePrefabs[index].transform.rotation);
    }
}
