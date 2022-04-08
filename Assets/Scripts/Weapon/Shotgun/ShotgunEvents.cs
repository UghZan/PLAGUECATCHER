using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEvents : MonoBehaviour
{
    public ShotgunWeapon wp;
    private Player p;
    // Start is called before the first frame update
    void Start()
    {
        wp = GetComponentInParent<ShotgunWeapon>();
        p = GetComponentInParent<Player>();
    }

    public void SetReloadInProgress(int set)
    {
        if (set == 0)
            wp.ReloadComplete();
        GetComponent<Animator>().SetBool("reload", set != 0);
        wp.reloadInProgress = set != 0;
    }

    public void UpdateShotAmmo()
    {
        wp.audio.PlayOneShot(wp.shellIn);
        wp.currentAmmo++;
        p.AddItem(1, -1);
        GetComponent<Animator>().SetInteger("reload_shots", GetComponent<Animator>().GetInteger("reload_shots")-1);
    }

    public void ResetReloadProgress()
    {
        GetComponent<Animator>().SetBool("reload_progress", false);
    }

    public void ResetReloadInterrupt()
    {
        GetComponent<Animator>().SetBool("reload_interrupt", false);
    }

    public void PlayPump()
    {
        wp.audio.PlayOneShot(wp.pump);
    }
}
