using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppingSounds : MonoBehaviour
{
    PlayerController pc;
    public AudioClip[] effects;
    public AudioSource leg;
    public float playDelay;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isMoving)
        {
            if (timer <= 0)
            {
                leg.PlayOneShot(effects[Random.Range(0, effects.Length)]);
                timer = playDelay * (pc.isSprinting ? 0.8f : 1f);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            leg.Stop();
        }
    }
}
