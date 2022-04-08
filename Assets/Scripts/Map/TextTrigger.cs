using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour, IInteractable
{
    public string text;
    InfoControl c;
    public AudioClip interact;
    [SerializeField] AudioSource s;
    public void OnInteract(Player player)
    {
        if(interact != null) s.PlayOneShot(interact);
        c.NotifyText(3, text);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(s == null)
            s = GetComponent<AudioSource>();
        c = FindObjectOfType<InfoControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
