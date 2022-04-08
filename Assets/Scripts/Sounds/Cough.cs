using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cough : MonoBehaviour
{
    public AudioClip[] coughs;
    Player p;
    AudioSource s;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        s = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!p.isDead && p.currentContamination > 75f)
            if (timer <= 0)
            {
                s.PlayOneShot(coughs[Random.Range(0, coughs.Length)]);
                timer = Random.Range(3, 7);
            }
            else
                timer -= Time.deltaTime;
    }
}
