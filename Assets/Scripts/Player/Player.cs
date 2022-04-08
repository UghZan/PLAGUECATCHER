using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Resources")]
    static int maxFilters = 5;
    [SerializeField] static int currentFilters = 1;
    static int maxShotgunShells = 60;
    [SerializeField] static int currentShotgunShells = 10;
    static int maxFuel = 120;
    [SerializeField] static int currentFuel = 30;

    [Header("Collects")]
    public bool[] collects;
    /*0 - main key
     *1 - secret key
     *2 - keycard
     *3 - server key
     *4 - amogus
     */

    [Header("Stats")]
    public float currentContamination = 0;
    float maxContamination = 100;
    public int pickupRange = 5;

    [Header("Technical")]
    public float contaminationFactor = 0.25f;
    float contamTemp = 0;
    float iFrames = 0;
    public bool isDead = false;

    [Header("References")]
    public AudioClip beep;
    public AudioClip hurt;
    GasMask gm;
    GasMaskControl gmc;
    GlobalWeaponController gwc;
    [SerializeField] Light flashlight;
    public Camera pc;
    PlayerController plc;
    RecoilController rc;
    [SerializeField] AudioSource pickup;
    [SerializeField] AudioSource misc;
    InfoControl ic;

    // Start is called before the first frame update
    void Start()
    {
        plc = GetComponent<PlayerController>();
        pc = Camera.main;
        gm = GetComponentInChildren<GasMask>();
        gmc = GetComponentInChildren<GasMaskControl>();
        gwc = FindObjectOfType<GlobalWeaponController>();
        rc = GetComponentInChildren<RecoilController>();
        collects = new bool[6];
        gmc.UpdateValues(3, currentShotgunShells + ":" + maxShotgunShells);
        gmc.UpdateValues(0, (contaminationFactor * 10).ToString("F0") + "%");
        gmc.UpdateValues(4, currentFilters + ":" + maxFilters);
        gmc.UpdateValues(1, currentContamination.ToString("F0") + "%");
        ic = FindObjectOfType<InfoControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iFrames > 0)
            iFrames -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            gm.MaskControl();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            AttemptPickup();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
        if ((gm.filterBroke || !gm.maskOn) && currentContamination < maxContamination)
            currentContamination += Time.fixedDeltaTime * contaminationFactor * 0.75f;

        if(currentContamination >= maxContamination)
        {
            GameOver();
        }

        if (currentContamination - contamTemp >= 1)
        {
            if(currentContamination > 75)
                gmc.UpdateValues(1, currentContamination.ToString("F0") + "%!!!");
            else
            gmc.UpdateValues(1, currentContamination.ToString("F0") + "%");
            contamTemp = currentContamination;
        }

    }

    private void GameOver()
    {
        isDead = true;
        plc.canMove = false;
        ic.NotifyText(3, "CONTAMINATION CRITICAL. USER DECEASED.");
    }

    private void AttemptPickup()
    {
        Ray ray = pc.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if(Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (!hit.transform.TryGetComponent(out IInteractable i))
                return;

            i.OnInteract(this);
        }
    }

    public void PlayPickupSound(AudioClip sound)
    {
        pickup.PlayOneShot(sound);
    }

    public void TakeDamage(float damage, bool fire)
    {
        if (iFrames > 0)
            return;
        misc.PlayOneShot(hurt);
        currentContamination += damage;
        rc.RecoilPunch(Random.Range(-10, 10), Random.Range(25, 30));
        iFrames = 0.75f;
    }

    public string ContFactorAsString(float factor)
    {
        if (factor < 1)
            return "LOW";
        else if (factor < 2)
            return "MED";
        else if (factor < 4)
            return "HIGH";
        else
            return "!!!";
    }

    public void ChangeContamFactor(float newFactor)
    {
        misc.PlayOneShot(beep, 0.25f);
        contaminationFactor += newFactor;
        gmc.UpdateValues(0, (contaminationFactor*10).ToString("F0") + "%");
    }
    public int GetItem(int id)
    {
        switch (id)
        {
            case 0:
                return currentFilters;
            case 1:
                return currentShotgunShells;
            case 2:
                return currentFuel;
            default:
                return 0;
        }
    }

    public void AddItem(int id, int amount)
    {
        switch(id)
        {
            case 0:
                currentFilters = Mathf.Clamp(currentFilters + amount, 0, maxFilters);
                gmc.UpdateValues(4, currentFilters + ":" + maxFilters);
                break;
            case 1:
                currentShotgunShells = Mathf.Clamp(currentShotgunShells + amount, 0, maxShotgunShells);
                gmc.UpdateValues(3, currentShotgunShells + ":" + maxShotgunShells);
                break;
            case 2:
                currentFuel = Mathf.Clamp(currentFuel + amount, 0, maxFuel);
                gmc.UpdateValues(6, currentFuel + ":" + maxFuel);
                break;
            case 3:
                gwc.weaponsInInventory[1] = true;
                break;
            case 4:
                currentContamination = Mathf.Clamp(currentContamination - amount, 0, maxContamination);
                gmc.UpdateValues(1, currentContamination.ToString("F0") + "%");
                break;
        }
    }
}
