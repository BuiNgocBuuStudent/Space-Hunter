using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler _instance;
    public static ObjectPooler Instance => _instance;
    public GameObject _objectToPool;
    public int amountToPool;

    public List<GameObject> pooledObjects;

    Dictionary<GameObject, List<GameObject>> _poolDic = new Dictionary<GameObject, List<GameObject>>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        AddPooledObject();
    }

    //Pool for Boost
    public GameObject GetObject(GameObject prefab)
    {
        List<GameObject> listObj = new List<GameObject>();
        if (_poolDic.ContainsKey(prefab))
            listObj = _poolDic[prefab];
        else
            _poolDic.Add(prefab, listObj);

        foreach (GameObject obj in listObj)
        {
            if (obj.activeSelf)
                continue;
            return obj;
        }

        GameObject newObj = Instantiate(prefab, transform.position, Quaternion.identity);
        listObj.Add(newObj);
        return newObj;
    }

    public T Getcomp<T>(T prefab) where T : MonoBehaviour
    {
        return this.GetObject(prefab.gameObject).GetComponent<T>();
    }

    //Pool for Bullet
    /*Không dùng cách như boost vì bullet cần biết trước số lượng để hiển thị và cập nhập khi số lượng thay đổi
     */
    public void AddPooledObject()
    {
        pooledObjects.Clear();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(_objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        return null;
    }


}
