using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Vector3 openPos;
    public Vector3 closePos;
    public bool open;
    public float speed;

    float timer;
    Vector3 target;
    //public GameObject doorObject;
    // Start is called before the first frame update
    void Start()
    {
        target = closePos;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 1)
            timer += Time.deltaTime * speed;
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, timer);
    }

    public void SwitchStates(Player p)
    {
        timer = 0;
        open = !open;
        target = open ? openPos : closePos;
    }
}
