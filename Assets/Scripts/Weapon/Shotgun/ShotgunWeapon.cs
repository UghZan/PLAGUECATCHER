using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : WeaponController
{
    [Header("Shotgun Settings")]
    public int pellets;
    public AudioClip shot;
    public AudioClip pump;
    public AudioClip shellIn;

    [Header("Reload Settings")]
    public bool reloadInProgress;

    Material spark;
    Material blood;
    public override void ReloadComplete()
    {
        gmc.UpdateValues(5, currentAmmo + ":" + maxAmmo);
    }

    protected override void Init()
    {
        spark = GameManager.instance.sparkMat;
        blood = GameManager.instance.bloodMat;
    }

    protected override void PrimaryFire()
    {
        if (currentAmmo == 0 || !GlobalWeaponController.canUseWeapons)
            return;
        animator.SetBool("shoot", true);
        if (reloadInProgress)
            animator.SetBool("reload_interrupt", true);
        shootEffect.Play();
        audio.PlayOneShot(shot);
        wmc.Punch();
        for (int i = 0; i < pellets; i++)
        {
            Ray _ray = player.pc.ScreenPointToRay(new Vector2(Screen.width / 2 + Random.Range(-inaccuracyFactor, inaccuracyFactor), Screen.height / 2 + Random.Range(-inaccuracyFactor, inaccuracyFactor)));
            if(Physics.Raycast(_ray, out RaycastHit hit, shotRange))
            {
                if(hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(damage, false);
                }
                if (hit.transform.gameObject.tag != "Enemy")
                {
                    GameObject _bulletHole = GameManager.instance.bulletHoles.GetObject();
                    _bulletHole.transform.position = hit.point + hit.normal * 0.1f;
                    _bulletHole.transform.rotation = Quaternion.FromToRotation(_bulletHole.transform.up, hit.normal) * _bulletHole.transform.rotation;
                    _bulletHole.gameObject.SetActive(true);
                    Debug.Log(hit.transform.name);
                }

                if (Random.value < 0.5)
                {
                    GameObject _spark = GameManager.instance.sparks.GetObject();
                    ParticleSystemRenderer _r = _spark.GetComponent<ParticleSystemRenderer>();
                    if (hit.transform.gameObject.tag == "Enemy")
                        _r.material = blood;
                    else
                        _r.material = spark;
                    _spark.transform.position = hit.point + hit.normal * 0.1f;
                    _spark.transform.rotation = Quaternion.LookRotation(hit.normal);
                    _spark.gameObject.SetActive(true);
                }
            }    
        }
        currentAmmo--;
    }

    protected override void Reload()
    {
        if (!reloadInProgress)
        {
            animator.SetBool("reload_progress", false);
            reloadInProgress = true;
            animator.SetBool("reload", true);
            animator.SetInteger("reload_shots", Mathf.Min((maxAmmo - currentAmmo), player.GetItem(1)));
        }
        else
        {
            gmc.UpdateValues(5, currentAmmo + ":" + maxAmmo);
            animator.SetBool("reload_progress", true);
        }
    }

    protected override void SecondaryFire()
    {
    }
}
