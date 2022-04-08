using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamerWeapon : WeaponController
{
    [Header("Flamer Specific")]
    [SerializeField] ParticleSystem flames;
    FlamerInteract interact;
    bool isFiring;
    public bool isReloading;
    public override void ReloadComplete()
    {
    }

    protected override void Init()
    {
        interact = GetComponentInChildren<FlamerInteract>();
        interact.damage = damage;
    }

    protected override void PrimaryFire()
    {
        if (currentAmmo == 0)
            return;
        isFiring = true;
        flames.Play();
        currentAmmo--;
    }

    protected override void Reload()
    {
        if (!isReloading)
        {
            animator.SetBool("reload", true);
            isReloading = true;
        }
    }

    protected override void SecondaryFire()
    {
    }
    public void ResetAmmo()
    {
        player.AddItem(2, currentAmmo);
        int ammo = Mathf.Min(maxAmmo, player.GetItem(2));
        currentAmmo = ammo;
        player.AddItem(2, -ammo);
        gmc.UpdateValues(5, currentAmmo + ":" + maxAmmo);
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(Input.GetMouseButtonUp(0) || currentAmmo <= 0)
        {
            gmc.UpdateValues(5, currentAmmo + ":" + maxAmmo);
            isFiring = false;
            flames.Stop();
        }

        if (isFiring)
            audio.UnPause();
        else
            audio.Pause();
    }
}
