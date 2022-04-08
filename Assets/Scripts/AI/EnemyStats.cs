using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject damageTrigger;
    public float health = 10;
    public float stunTimer;

    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject damageAura;
    EnemyBrain brain;
    EnemySounds sounds;

    public void TakeDamage(float damage, bool fire)
    {
        if (health < 0)
            return;
        if(fire)
        {
            health -= damage * 0.5f;
            stunTimer += damage * 0.001f;
        }
        else
        {
            health -= damage;
            stunTimer += damage * 0.02f;
        }
        if (health < 0)
        {
            ParticleSystem death = GetComponentInChildren<ParticleSystem>();
            ParticleSystem death2 = transform.GetChild(2).GetComponent<ParticleSystem>();

            death.Play();
            death.transform.parent = null;

            death2.Play();
            death2.transform.parent = null;

            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            transform.GetChild(0).GetChild(2).GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            sounds.Death();

            damageAura.transform.parent = null;
            damageAura.GetComponent<ContaminatedZone>().ownerDead = true;

            Destroy(this);
            GetComponent<Collider>().enabled = false;
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(brain);
            Destroy(death.gameObject, 10);
            Destroy(death2.gameObject, 10);
            Destroy(gameObject, 5);
        }
        if (brain.GetState() != State.CHASE && brain.GetState() != State.ATTACK)
        {
            transform.rotation = brain.GetNextLookingRotation();
            brain.lastKnownPos = brain.GetRandomLocationFromPoint(transform.position, 5);
            brain.ChangeState(State.SEARCH);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        brain = GetComponent<EnemyBrain>();
        sounds = GetComponent<EnemySounds>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stunTimer > 0)
            stunTimer-= Time.deltaTime;
    }

}
