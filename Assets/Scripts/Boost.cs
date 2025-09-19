using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] List<Vector3> _spawnPoints;
    public void Init()
    {
        _spawnPoints = GameManager.Instance.spawnPoints;
        transform.position = GetRanDomPos();
    }
    private Vector3 GetRanDomPos()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[index];

    }
}
