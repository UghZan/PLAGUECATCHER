using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Quaternion openRotation;
    public Quaternion closeRotation;
    public bool open;
    public float speed;

    Quaternion target;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * speed);
    }

    public void SwitchStates(Player p)
    {
        open = !open;
        target = open ? openRotation : closeRotation;
    }
}
