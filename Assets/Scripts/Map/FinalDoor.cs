using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalDoor : MonoBehaviour, IInteractable
{
    public UnityEvent onInteract;
    public int requires = 5;
    public string messageNotYet = "NOT YET";
    public string messageFinal = "CONGRATS! You got the true ending. You can delete the game now.";

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

    public void OnInteract(Player p)
    {
        if (!p.collects[5])
        {
            c.NotifyText(3, messageNotYet);
        }
        else
        {
            c.NotifyText(3, messageFinal, 100);
        }
    }
}
