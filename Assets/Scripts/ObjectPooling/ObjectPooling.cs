using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    private static ObjectPooling _instance;
    private static Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    [SerializeField] private GameObject GraveyardObj;

    public static ObjectPooling Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<ObjectPooling>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("ObjectPooling");
                    _instance = obj.AddComponent<ObjectPooling>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject InstantiateObject(GameObject prefab)
    {
        string key = prefab.name;
        GameObject obj = null;

        if (poolDictionary.TryGetValue(key, out Queue<GameObject> pool))
        {
            if (pool.Count > 0)
            {
                obj = pool.Dequeue();
            }
        }

        if (obj == null)
        {
            obj = Instantiate(prefab);
            obj.name = prefab.name;
        }
        
        obj.SetActive(true);
        obj.transform.SetParent(null);
        return obj;
    }

    public GameObject InstantiateObject(GameObject prefab, Vector3 position)
    {
        GameObject obj = InstantiateObject(prefab);
        obj.transform.position = position;
        return obj;
    }

    public GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = InstantiateObject(prefab, position);
        obj.transform.rotation = rotation;
        return obj;
    }

    public GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject obj = InstantiateObject(prefab, position, rotation);
        obj.transform.SetParent(parent);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        string key = obj.name;
        if (!poolDictionary.TryGetValue(key, out Queue<GameObject> pool))
        {
            pool = new Queue<GameObject>();
            poolDictionary[key] = pool;
        }

        pool.Enqueue(obj);
        if (GraveyardObj != null)
        {
            obj.transform.SetParent(GraveyardObj.transform);
        }
        obj.SetActive(false);
    }
}