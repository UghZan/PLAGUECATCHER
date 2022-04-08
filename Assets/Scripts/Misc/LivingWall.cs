using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingWall : MonoBehaviour, IDamageable
{
    public float health = 100;
    float flameTimer = 0f;
    [SerializeField] ParticleSystem main;
    [SerializeField] ParticleSystem burnEffect;
    public void TakeDamage(float damage, bool fire)
    {
        if(fire)
        {
            health -= damage;
            flameTimer = 2;
        }
        else
        {
            health -= damage * 0.1f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        flameTimer -= Time.deltaTime;
        if(flameTimer > 0)
        {
            if(!burnEffect.isPlaying)burnEffect.Play();
            health -= Time.deltaTime;
        }
        else
        {
            if(burnEffect.isPlaying)burnEffect.Stop();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            main.transform.parent = null;
            main.Stop();
            ParticleSystem.MainModule mainMod = main.main;
            mainMod.gravityModifier = 0.5f;

            Destroy(main.gameObject, 10);
        }
    }
}
