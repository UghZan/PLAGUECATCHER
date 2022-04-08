using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrigger : MonoBehaviour, IInteractable
{
    public GameObject what;

    public void OnInteract(Player p)
    {
        Debug.Log("es");
        what.SetActive(true);
    }
}
