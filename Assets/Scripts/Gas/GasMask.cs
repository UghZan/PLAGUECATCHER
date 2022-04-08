using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasMask : MonoBehaviour
{
    [Header("Movement Attributes")]
    public float bobAmount;
    public float bobSpeed;

    public float breathAmount;
    public float breathSpeed;

    [Header("Gameplay Attributes")]
    public bool maskOn = false;
    public float currentFilterResource = 120f;
    float maxFilterResource = 120f;
    public bool filterBroke = false;

    float tempResource = 0;

    [Header("Sounds")]
    public AudioClip on;
    public AudioClip off;
    public AudioClip filtr;

    [SerializeField] AudioSource audio;
    [SerializeField] AudioSource filtr_audio;
    Player p;
    Vector3 position;
    Animator anim;
    GasMaskControl gmc;

    // Use this for initialization
    void Start()
    {
        tempResource = currentFilterResource;
        p = GetComponentInParent<Player>();
        position = transform.localPosition;
        anim = GetComponentInChildren<Animator>();
        gmc = GetComponent<GasMaskControl>();
        gmc.UpdateValues(2, (currentFilterResource / maxFilterResource * 100).ToString("F0") + "%");
    }

    void Update()
    {
        if (maskOn && currentFilterResource > 0)
            currentFilterResource -= Time.deltaTime * p.contaminationFactor * 0.75f;

        if (currentFilterResource <= 0)
            filterBroke = true;
        else
            filterBroke = false;

        if (currentFilterResource + 10 < tempResource)
        {
            tempResource = currentFilterResource;
            gmc.UpdateValues(2, (currentFilterResource / maxFilterResource * 100).ToString("F0") + "%");
        }

        if(currentFilterResource <= 5f && maskOn)
        {
            filtr_audio.UnPause();
        }
        else
        {
            filtr_audio.Pause();
        }
        

        if (Input.GetKeyDown(KeyCode.Z) && maskOn && p.GetItem(0) > 0)
            ChangeFilter();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            Walk();
        else
            Breath();
    }

    public void Walk()
    {
        float xBob = 2 * Mathf.Sin(Time.time * bobSpeed + Mathf.PI / 2) * bobAmount;
        float yBob = Mathf.Sin(Time.time * 2 * bobSpeed) * bobAmount;

        Vector3 final = new Vector3(position.x + xBob, position.y + yBob, position.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, 0.1f*Time.deltaTime);
    }

    public void Breath()
    {
        float yBob = Mathf.Sin(Time.time * 2 * breathSpeed) * breathAmount;

        Vector3 final = new Vector3(position.x, position.y + yBob, position.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, 0.1f*Time.deltaTime);
    }

    public void ResetFilter()
    {
        currentFilterResource = maxFilterResource;
        gmc.UpdateValues(2, (currentFilterResource / maxFilterResource * 100).ToString("F0") + "%");
        tempResource = currentFilterResource;
    }

    public void ChangeFilter()
    {
        gmc.UpdateValues(2, "NONE", 0);
        anim.SetBool("filter", true);
        p.AddItem(0, -1);
    }

    public void PlaySound(int which)
    {
        switch(which)
        {
            case 0:
                audio.PlayOneShot(on);
                break;
            case 1:
                audio.PlayOneShot(off);
                break;
            case 2:
                audio.PlayOneShot(filtr);
                break;
        }
    }

    public void MaskControl()
    {
        maskOn = !maskOn;
        anim.SetBool("mask_on", maskOn);
    }
}
