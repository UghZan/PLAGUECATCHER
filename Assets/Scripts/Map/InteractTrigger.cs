using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : MonoBehaviour, IInteractable
{
    public UnityEvent onInteract;
    public int requires = -1;
    public int messageType = 1;
    public string messageOverride = "";
    public AudioClip failed;
    public AudioClip succeed;
    [SerializeField] AudioSource s;

    InfoControl c;
    // Start is called before the first frame update
    void Start()
    {
        if (s == null)
            s = GetComponent<AudioSource>();
        c = FindObjectOfType<InfoControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract(Player p)
    {
        if (requires >= 0)
            if (!p.collects[requires])
            {
                if(failed != null) s.PlayOneShot(failed);
                c.NotifyText(messageType, messageOverride);
                return;
            }
        if (succeed != null) s.PlayOneShot(succeed);
        onInteract.Invoke();
    }
}
