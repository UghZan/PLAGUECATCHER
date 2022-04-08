using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioSource walking;
    public AudioSource voice;
    public AudioClip deathSound;
    public AudioClip[] alarmSounds;
    public AudioClip[] idleSounds;
    public AudioClip[] chaseSounds;

    float timer = 0;
    EnemyBrain brain;
    // Start is called before the first frame update
    void Start()
    {
        brain = GetComponent<EnemyBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime * (brain.GetState() == State.CHASE ? 1.5f : 1f);
        else
        {
            if (brain.GetState() == State.IDLE || brain.GetState() == State.PATROL)
                IdleSound();
            else if (brain.GetState() == State.CHASE)
                ChaseSound();
        }

        if(brain.isMoving)
        {
            walking.UnPause();
        }
        else
        {
            walking.Pause();
        }
    }

    public void AlarmSound()
    {
        voice.PlayOneShot(alarmSounds[Random.Range(0,alarmSounds.Length)]);
    }

    public void IdleSound()
    {
        voice.PlayOneShot(idleSounds[Random.Range(0, idleSounds.Length)]);
        timer = Random.Range(4, 12);
    }

    public void ChaseSound()
    {
        voice.PlayOneShot(chaseSounds[Random.Range(0, chaseSounds.Length)]);
        timer = Random.Range(2, 6);
    }

    public void Death()
    {
        walking.Stop();
        voice.maxDistance = 100;
        voice.PlayOneShot(deathSound);
        timer = 999;
        Destroy(gameObject, 10);
    }
}
