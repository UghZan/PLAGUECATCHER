using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalWeaponController : MonoBehaviour
{
    public static bool canUseWeapons = true;
    public GameObject hand;
    public GameObject[] weapons;
    public bool[] weaponsInInventory;
    public int currentWeapon;

    public GasMaskControl gmc;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (currentWeapon == 1 && weaponsInInventory[0])
            { 
                currentWeapon = 0;
            }
            else if (currentWeapon == 0 && weaponsInInventory[1])
            {
                currentWeapon = 1;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == currentWeapon)
                {
                    weapons[i].SetActive(true);
                    weapons[i].GetComponent<WeaponController>().ReloadComplete();
                }
                else
                {
                    weapons[i].SetActive(false);
                }
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (currentWeapon == 0 && weaponsInInventory[1])
            {
                currentWeapon = 1;
            }
            else if (currentWeapon == 1 && weaponsInInventory[0])
            {
                currentWeapon = 0;
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == currentWeapon)
                {
                    weapons[i].SetActive(true);
                    weapons[i].GetComponent<WeaponController>().ReloadComplete();
                }
                else
                {
                    weapons[i].SetActive(false);
                }
            }

        }
    }

}
