using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Common Variables")]
    public Material sparkMat;
    public Material bloodMat;

    [Header("Effects")]
    public GameObject bulletHole;
    public GameObject spark;

    [Header("Pools")]
    public GameObjectPool bulletHoles;
    public GameObjectPool sparks;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletHoles = new GameObjectPool(100, bulletHole);
        sparks = new GameObjectPool(15, spark);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
