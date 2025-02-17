using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField] private List<Vector3> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameManager.Instance.spawnPoints;

        transform.position = RanDomPos();
    }

    // Update is called once per frame
    private Vector3 RanDomPos()
    {
        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index];

    }
}
