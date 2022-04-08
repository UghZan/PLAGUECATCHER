using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject notebook;
    public Material blackMaterial;
    public AudioSource s;
    public GameObject jerma;
    public Material jermaNotSus;
    public AudioClip goodMusic;
    public GameObject enemies;
    InfoControl c;
    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<InfoControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinalScene()
    {
        Player p = FindObjectOfType<Player>();
        notebook.GetComponent<MeshRenderer>().material = blackMaterial;
        s.Play();
        if(p.collects[4])
        {
            jerma.SetActive(true);
            c.NotifyText(3, "OH NO! YOU'RE- WAIT. YOU'RE NOT SUS. CONTINUE ON.");
            jerma.GetComponent<AudioSource>().clip = goodMusic;
            jerma.GetComponent<AudioSource>().Play();
            jerma.GetComponent<MeshRenderer>().material = jermaNotSus;
            p.collects[5] = true;
        }
        else
        {
            jerma.GetComponent<SlidingDoor>().enabled = false;
            jerma.SetActive(true);
            enemies.SetActive(true);
            c.NotifyText(3, "OH NO! YOU'RE SUS! TIME FOR EJECTION! :)", 999);
        }
    }
}
