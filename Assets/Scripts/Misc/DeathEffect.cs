using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set(SkinnedMeshRenderer s)
    {
        SkinnedMeshRenderer m = Instantiate(s);
        ParticleSystem.ShapeModule shape = GetComponent<ParticleSystem>().shape;
        shape.skinnedMeshRenderer = m;
    }
}
