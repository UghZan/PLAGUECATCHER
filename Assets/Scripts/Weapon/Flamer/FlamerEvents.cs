using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamerEvents : MonoBehaviour
{
    FlamerWeapon w;
    // Start is called before the first frame update
    void Start()
    {
        w = GetComponentInParent<FlamerWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetReload()
    {
        w.ReloadComplete();
        GetComponent<Animator>().SetBool("reload", false);
        w.isReloading = false;
        w.ResetAmmo();
    }
}
