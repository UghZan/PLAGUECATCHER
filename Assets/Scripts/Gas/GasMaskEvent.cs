using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMaskEvent : MonoBehaviour
{
    public void ControlVisibilty(int visible)
    {
        foreach (Transform r in transform)
        {
            r.gameObject.SetActive(visible != 0);
        }
    }

    public void ControlUseWeapons(int canUse)
    {
        GlobalWeaponController.canUseWeapons = canUse != 0;
    }

    public void SetFilter()
    {
        GetComponentInParent<GasMask>().ResetFilter();
    }

    public void PlaySound(int num)
    {
        GetComponentInParent<GasMask>().PlaySound(num);
    }
}
