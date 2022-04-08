using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeycardReader : MonoBehaviour, IInteractable
{
    public UnityEvent onInteract;

    bool yes;
    public GameObject lilPipi;
    public Material good;
    AudioSource s;
    public AudioClip amongus;
    InfoControl c;
    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<InfoControl>();
        s = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract(Player p)
    {
        if (!p.collects[2])
            c.NotifyText(2);
        else
            Change();
    }

    public void Change()
    {
        onInteract.Invoke();
        if (!yes)
        {
            s.PlayOneShot(amongus);
            lilPipi.GetComponent<MeshRenderer>().material = good;
            yes = true;
        }
    }
}
