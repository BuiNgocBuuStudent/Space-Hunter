using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{

    [SerializeField] List<Vector3> _spawnPoints;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Init()
    {
        _spawnPoints = GameManager.Instance.spawnPoints;
        transform.position = RanDomPos();
    }
    // Update is called once per frame
    private Vector3 RanDomPos()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        return _spawnPoints[index];

    }
}
