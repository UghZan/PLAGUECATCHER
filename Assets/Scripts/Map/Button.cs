using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    bool yes;
    public GameObject button;
    public Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Event()
    {
        if (!yes)
        {
            button.transform.position = newPos;
            GetComponent<AudioSource>().Play();
        }
    }
}
