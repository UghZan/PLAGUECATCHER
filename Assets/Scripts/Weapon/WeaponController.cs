using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    public string weaponName;

    [Header("Weapon Stats")]
    public float damage;
    public float shootSpeed;
    public float inaccuracyFactor;
    public bool isAutomatic;
    public int maxAmmo;
    public float shotRange;

    [Header("Visual Controls")]
    public float cameraRecoil_x;
    public float cameraRecoil_y;

    protected float recoil;
    [SerializeField] public int currentAmmo;

    [Header("References")]
    protected Player player;
    protected GasMaskControl gmc;
    public Animator animator;
    public WeaponMovementController wmc;
    public ParticleSystem shootEffect;
    public AudioSource audio;

    protected float timer = 0f;

    protected abstract void Init();
    protected abstract void PrimaryFire();
    protected abstract void SecondaryFire();
    protected abstract void Reload();
    public abstract void ReloadComplete();

    void Start()
    {
        gmc = FindObjectOfType<GasMaskControl>();
        player = GetComponentInParent<Player>();
        animator = GetComponentInChildren<Animator>();
        wmc = GetComponent<WeaponMovementController>();
        Init();
    }

    void OnEnable()
    {
        gmc.UpdateValues(5, currentAmmo + ":" + maxAmmo);
    }

    protected virtual void Update()
    {
        if (player.isDead)
        {
            Destroy(wmc);
            return;
        }
        timer += Time.deltaTime;
        if (timer > 1 / shootSpeed)
        {
            if (isAutomatic)
            {
                if (Input.GetMouseButton(0))
                {
                    PrimaryFire();
                    timer = 0;

                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PrimaryFire();
                    timer = 0;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }


    }
}
