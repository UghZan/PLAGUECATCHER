using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private GameObject objectPrefab = null;
    private List<GameObject> poolOfObjects = new List<GameObject>();

    public GameObjectPool(int initialPoolSize, GameObject prefab)
    {
        objectPrefab = prefab;
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject newObject = Object.Instantiate(prefab);
            newObject.gameObject.SetActive(false);
            poolOfObjects.Add(newObject);
        }
    }

    public GameObject GetObject()
    {
        GameObject returnObject;
        if (poolOfObjects.Count == 0)
        {
            returnObject = Object.Instantiate(objectPrefab);
        }
        else
        {
            returnObject = poolOfObjects[poolOfObjects.Count - 1];
            poolOfObjects.RemoveAt(poolOfObjects.Count - 1);
        }
        return returnObject;
    }

    public void ReturnObject(GameObject returnedObject)
    {
        poolOfObjects.Add(returnedObject);
        returnedObject.SetActive(false);
    }
}