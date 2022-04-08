using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollideTrigger : MonoBehaviour
{
    public UnityEvent onInteract;
    private void OnTriggerEnter(Collider other)
    {
        onInteract.Invoke();
    }
}
