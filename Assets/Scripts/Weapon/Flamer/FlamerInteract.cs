using System.Collections.Generic;
using UnityEngine;

public class FlamerInteract : MonoBehaviour
{
    public float damage;
    ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    public void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    public void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Player p))
            return;
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        for(int i = 0; i < numCollisionEvents; i++)
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage, true);
        }
    }
}