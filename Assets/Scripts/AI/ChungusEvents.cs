using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChungusEvents : MonoBehaviour
{
    public GameObject col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAttackingTrigger(int state)
    {
        col.GetComponent<Collider>().enabled = state != 0;
    }

    void SetAttackInProgress(int state)
    {
        GetComponent<Animator>().SetBool("attack", state != 0);
    }
}
