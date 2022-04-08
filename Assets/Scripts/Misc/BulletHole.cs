using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    [SerializeField] float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            GameManager.instance.bulletHoles.ReturnObject(gameObject);
    }
}
