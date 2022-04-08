using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaminatedZone : MonoBehaviour
{
    public bool ownerDead;//for correctly decreasing player contamination if owner dies
    public float contaminationLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Player>())
            return;

        other.GetComponent<Player>().ChangeContamFactor(contaminationLevel);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Player>())
            return;

        other.GetComponent<Player>().ChangeContamFactor(-contaminationLevel);
        if(ownerDead) Destroy(gameObject);
    }
}
