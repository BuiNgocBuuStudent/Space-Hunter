using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private List<Vector3> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        spawnPoints = gameManager.spawnPoints;

        transform.position = RanDomPos();
    }

    // Update is called once per frame
    private Vector3 RanDomPos()
    {
        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index];

    }
}
