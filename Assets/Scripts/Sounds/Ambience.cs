using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    public AudioClip[] ambienceSounds;
    public GameObject player;
    public GameObject source;

    public float minDelay = 50;
    public float maxDelay = 200;

    float delay;

    private void Start()
    {
        delay = Random.Range(5, 10);
    }
    private void Update()
    {
        if(delay > 0)
        delay -= Time.deltaTime;
        else
        {
            source.transform.position = player.transform.position + Random.onUnitSphere * Random.Range(10, 40);
            source.GetComponent<AudioSource>().PlayOneShot(ambienceSounds[Random.Range(0, ambienceSounds.Length)]);
			source.GetComponent<AudioSource>().volume = Random.Range(0.65f, 1.0f);
			source.GetComponent<AudioSource>().pitch = Random.Range(0.75f, 1.2f);
            delay = Random.Range(minDelay, maxDelay);
        }
    }
}
